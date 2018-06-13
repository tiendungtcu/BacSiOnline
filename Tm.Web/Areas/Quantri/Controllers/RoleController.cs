using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TM.Web.Models;

namespace TM.Web.Areas.Quantri.Controllers
{
    public class RoleController : QuantriBaseController
    {
        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);
        }
        // 
        [AllowAnonymous]
        public ActionResult TabView()
        {
            return PartialView("RolePartial");
        }
        public JsonResult ListAll()
        {
            var role = context.Roles.Select(d => new { Id = d.Id, Name = d.Name,Description = d.Description })
                                .OrderBy(d => d.Id)
                                .ToArray();
            return Json(role, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Lấy danh sách các Roles trong cơ sở dữ liệu và trả về dạng JSon
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ListAllPaging(int? page = 1, int? rows = 10)
        {
            int pageIndex = page.HasValue ? (int)page : 1;
            int pageSize = rows.HasValue ? (int)rows : 10;
            int skip = (pageSize * (pageIndex - 1));
            int total = context.Roles.Count();
            if (skip > total)
            {
                return Json(new { isError = true, errorMsg = "Không có dữ liệu" });
            }

            var Roles = context.Roles.Select(r => new { r.Id, r.Name, r.Description  })
                                     .OrderBy(r => r.Id)
                                     .Skip(skip)
                                     .Take(pageSize)
                                     .ToArray();
            return Json(new { total = total, rows = Roles });
        }
        /// <summary>
        /// Tạo mới một Role
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Create(CustomRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            var addedRole = context.Roles
                .Where(r => r.Name == Role.Name)
                .Select(r => new { r.Id, r.Name,r.Description });
            return Json(addedRole.FirstOrDefault());
        }
        /// <summary>
        /// Cập nhật Role
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update([Bind(Include = "Id,Name,Description")]CustomRole Role)
        {
            context.Entry(Role).State = EntityState.Modified;
            context.SaveChanges();
            return Json(new { isNewRecord = false, Id = Role.Id, Name = Role.Name, Description = Role.Description });
        }
        /// <summary>
        /// Xóa bỏ một Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Destroy(int id)
        {
            CustomRole role = context.Roles.Find(id);
            // Nếu không tìm thấy role nào có id này thì thông báo
            // không tìm thấy
            if (role == null)
            {
                return Json(new { isError = true, errorMsg = "Nhóm này không tồn tại" });
            }
            // tìm và bỏ role này cho tất cả người dùng
            foreach (var user in context.Users.ToList())
            {
                if (UserManager.IsInRole(user.Id, role.Name))
                {
                    UserManager.RemoveFromRole(id, role.Name);
                }
            }
            // sau đó mới xóa bỏ role này                    
            context.Roles.Remove(role);
            context.SaveChanges();
            return Json(new { success = true });
        }
    }
}