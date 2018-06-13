using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tm.Data.Functions;
using Tm.Data.Models;
using TM.Web.Areas.Quantri.Controllers;

namespace TM.Web.Areas.Quantri.Controllers
{
    public class NotifyTypeController : QuantriBaseController
    {
        [AllowAnonymous]
        public ActionResult TabView()
        {
            return PartialView("NotifyTypePartial");
        }
        /// <summary>
        /// Lấy danh sách trieu chung nạp vào combobox
        /// </summary>
        /// <returns></returns>
        public JsonResult ListAll()
        {
            var entity = new NotifyTypeDao().ListAll();
            return Json(entity, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Lấy danh sách các triệu chứng trong cơ sở dữ liệu và trả về dạng JSon
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ListAllPaging(int? page = 1, int? rows = 10)
        {
            int pageIndex = page.HasValue ? (int)page : 1;
            int pageSize = rows.HasValue ? (int)rows : 10;
            int total = 0;
            var result = new NotifyTypeDao().ListAllPaging(out total, pageIndex, pageSize);
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
        public JsonResult Create(TM_NotifyType entity)
        {
            byte SymptomId = new NotifyTypeDao().Create(entity);
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
        public JsonResult Update(TM_NotifyType entity)
        {
            bool ok = new NotifyTypeDao().Update(entity);
            if (ok)
            {
                return Json(new
                {
                    isNewRecord = false,
                    Id = entity.Id,
                    Name = entity.Name,                   
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
            bool ok = new NotifyTypeDao().Delete(id);
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