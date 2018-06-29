using System.Web.Mvc;
using Tm.Data.Functions;
using Tm.Data.Models;
using Tm.Data.ViewModels;
using TM.Web.Controllers;

namespace TM.Web.Areas.Quantri.Controllers
{
    public class NotificationController :  CommonBaseController
    {
        [AllowAnonymous]
        public ActionResult TabView()
        {
            return PartialView("NotificationPartial");
        }
        /// <summary>
        /// Lấy danh sách trieu chung nạp vào combobox
        /// </summary>
        /// <returns></returns>
        public JsonResult ListAll()
        {
            var entity = new NotificationDao().ListAll();
            return Json(entity, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Lấy danh sách các triệu chứng trong cơ sở dữ liệu và trả về dạng JSon
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ListAllPaging(string keyword,int? typeId,int? userId,int? page = 1, int? rows = 10)
        {
            int pageIndex = page.HasValue ? (int)page : 1;
            int pageSize = rows.HasValue ? (int)rows : 10;
            int type = typeId == null ? 0 : (int)typeId;
            int receiver = userId == null ? 0 : (int)userId;
            int total = 0;
            // var result = new NotificationDao().ListAllPaging(out total, pageIndex, pageSize);
            var result = new NotificationDao().ListAllPaging(out total,keyword,type,receiver, pageIndex, pageSize);
            if (result == null)
            {
                return NotifyError("Liệt kê");
            }
            return Json(new { total = total, rows = result });
        }
        /// <summary>
        /// Thêm một đơn vị mới
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Create(TM_Notification entity)
        {
            long SymptomId = new NotificationDao().Create(entity);
            entity.Id = SymptomId;
            if (SymptomId > 0)
            {
                return Json(entity);
            }
            else
            {
                return NotifyError("Tạo");
            }

        }
        /// <summary>
        /// Cập nhật chi tiết đơn vị
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(NotificationDetail entity)
        {
            bool ok = new NotificationDao().Update(entity);
            if (ok)
            {
                return Json(new
                {
                    isNewRecord = false,
                    Id = entity.Id,
                    Title = entity.Title,
                    Contents = entity.Contents,
                    Type = entity.Type,
                    TypeName = entity.TypeName,
                    ReceiverId = entity.ReceiverId,
                    Receiver = entity.Receiver,
                    Link = entity.Link,
                    Status = entity.Status
                });
            }
            else
            {
                return NotifyError("Cập nhật"); ;
            }

        }
        /// <summary>
        /// Xóa bỏ một đơn vị
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Destroy(int id)
        {
            bool ok = new NotificationDao().Delete(id);
            if (ok)
            {
                return Json(new { success = true });
            }
            else
            {
                return NotifyError("xóa");
            }
        }
    }
}