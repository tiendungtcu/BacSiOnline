using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TM.Web.Models;
using Tm.Data.Models;
using TM.Web.Areas.Quantri.Models;
using System.Threading.Tasks;

namespace TM.Web.Areas.Quantri.Controllers
{
    public class UserManagerController : QuantriBaseController
    {
        /// <summary>
        /// Trả về Partial view 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult TabView()
        {
            return PartialView("UserManagePartial");
        }

        /// <summary>
        /// Lấy danh sách tên người dùng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public JsonResult ListAll()
        {          
            var users = context.Users
                         .Select (u=> new
                         {
                             Id = u.Id,
                             UserName = u.UserName,                         
                             FullName = string.IsNullOrEmpty(u.FullName)?u.UserName:u.FullName                          
                         }).ToArray();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Lấy danh sách tên người dùng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public JsonResult ListAllDoctor(string role)
        {
            var roles = context.Roles.Where(r => r.Name == role);
            if (roles.Any())
            {
                var roleId = roles.First().Id;
                var result = from user in context.Users
                             where user.Roles.Any(r => r.RoleId == roleId)
                             select new {Id = user.Id,UserName = user.UserName } ;
                var result1 = result.Select(r => new { id = r.Id, text = r.UserName }).ToArray();
                return Json(new { results = result1 }, JsonRequestBehavior.AllowGet);
            }
            return NotifyError("Xem");
        }
        /// <summary>
        /// Lấy danh sách người dùng có phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public JsonResult ListAllPaging(int? page = 1, int? rows = 10)
        {
            int pageIndex = page.HasValue ? (int)page : 1;
            int pageSize = rows.HasValue ? (int)rows : 10;
            int skip = (pageSize * (pageIndex - 1));
            int total = context.Users.Count();
            if (skip > total)
            {
                return Json(new { isError = true, errorMsg = "Không có dữ liệu" });
            }
            var users = (from user in context.Users
                         from r in context.Roles
                         from r2 in r.Users.Where(x => x.UserId == user.Id)
                         orderby user.Id

                         select new
                         {
                             Id = user.Id,
                             UserName = user.UserName,
                             RoleId = (int?)r.Id,
                             Role = r.Name,
                             FullName = user.FullName,
                             Email = user.Email,
                         }).Skip(skip).Take(pageSize);
            return Json(new { total = total, rows = users }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Xóa tài khoản người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JsonResult> Destroy(int id)
        {
            // Find user by id
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Json(new { isError = true, errorMsg = "Người dùng không tồn tại." });
            }
            // get user logins
            var logins = user.Logins;
            // get user roles
            var rolesForUser = await _userManager.GetRolesAsync(id);

            using (var transaction = context.Database.BeginTransaction())
            {
                foreach (var login in logins.ToList())
                {
                    // remove user login
                    await _userManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                }

                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        var result = await _userManager.RemoveFromRoleAsync(user.Id, item);
                    }
                }

                await _userManager.DeleteAsync(user);
                transaction.Commit();
            }


            /*

            ApplicationUser user = context.Users.Find(id);
            // Nếu không tìm thấy người dùng nào có id này thì thông báo
            // không tìm thấy
            if (user == null)
            {
                return Json(new { isError = true, errorMsg = "Người dùng không tồn tại." });
            }
            // tìm và bỏ các role đã gán cho người dùng này
            foreach (var role in context.Roles.ToList())
            {
                if (UserManager.IsInRole(user.Id, role.Name))
                {
                    UserManager.RemoveFromRole(id, role.Name);
                }
            }
            // sau đó mới xóa bỏ user này                    
            context.Users.Remove(user);
            context.SaveChanges();
            */
            return Json(new { success = true });
        }
        public JsonResult ResetPassword(int id)
        {
            Web.Models.ApplicationUser user = context.Users.Find(id);
            // Nếu không tìm thấy người dùng nào có id này thì thông báo
            // không tìm thấy
            if (user == null)
            {
                return Json(new { isError = true, errorMsg = "Tài khoản này không tồn tại." });
            }
            if (!UserManager.RemovePassword(user.Id).Succeeded)
                return Json(new { isError = true, errorMsg = "Không xóa được mật khẩu cũ." });
            var result = UserManager.AddPassword(user.Id, "123456");
            if (result.Succeeded)
            {
                return Json(new { success = true });
            }
            else
                return Json(new { isError = true, errorMsg = "Không reset được mật khẩu." });

        }
        [HttpPost]
        public JsonResult Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                // tạo người dùng mới

                var result = UserManager.Create(user, model.PassWord);
                if (result.Succeeded)
                {
                    //string roleName = context.Roles.Find(model.UserRole)
                    //nếu tạo xong người dùng thì gán role
                    if (model.Role.HasValue)
                    {
                        this.UserManager.AddToRole(user.Id, context.Roles.Find(model.Role).Name);
                    }

                    // Lấy thông tin người dùng vừa tạo trả về cho client
                    var addedUser = from users in context.Users
                                    from r in context.Roles
                                    where (users.Id == user.Id)
                                    select new
                                    {
                                        Id = user.Id,
                                        UserName = user.UserName,
                                        RoleId = (int?)r.Id,
                                        Role = r.Name,
                                        FullName = user.FullName,
                                        Email = user.Email
                                    };
                    return Json(addedUser.FirstOrDefault());
                }
                else return Json(new { isError = true, errorMsg = "Không thêm được người dùng." });
            }
            else
                // nếu có lỗi thì thông báo về client
                return Json(new { isError = true, errorMsg = "Dữ liệu không phù hợp." });
        }
        [HttpPost]
        public JsonResult Update(FormCollection fc, ApplicationUser user)
        {
            // Xóa các Role cũ của người dùng này
            var roles = UserManager.GetRoles(user.Id);
            var result = UserManager.RemoveFromRoles(user.Id, roles.ToArray());
            if (!result.Succeeded)
            {
                return Json(new { isError = true, errorMsg = "Không xóa được user Role." });
            }
            // Gán Role mới
            int? roleid = null;
            string roleName = "";
            if (fc["RoleId"] != null)
            {
                roleid = int.Parse(fc["RoleId"]);
                roleName = context.Roles.Find(roleid).Name;
                result = UserManager.AddToRole(user.Id, roleName);
                if (!result.Succeeded)
                {
                    return Json(new { isError = true, errorMsg = "Không cập nhật được user Role." });
                }
            }
            // Lấy thông tin user đang chỉnh sửa
            var eUser = context.Users.Find(user.Id);
            // Cập nhật các thuộc tính đã thay đổi
            eUser.Email = user.Email;
            eUser.FullName = user.FullName;
            context.Entry(eUser).State = EntityState.Modified;
            context.SaveChanges();

            return Json(new
            {
                isNewRecord = false,
                Id = user.Id,
                UserName = user.UserName,
                RoleId = roleid,
                Role = roleName,
                FullName = user.FullName,
                Email = user.Email
            });
        }
    }
}