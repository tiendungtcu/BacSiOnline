using System;
using System.Web.Mvc;
using Tm.Data.Models;
using Tm.Data.Functions;


namespace TM.Web.Areas.Quantri.Controllers
{
    public class ParamController : QuantriBaseController
    {
        // 
        [AllowAnonymous]
        public ActionResult TabView()
        {
            return PartialView("ParamPartial");
        }
        /// <summary>
        /// Lấy danh sách đơn vị nạp vào combobox
        /// </summary>
        /// <returns></returns>
        public JsonResult ListAll()
        {
            var param = new MeasureParamDao().ListAll();
            if (param == null)
            {
                return NotifyError("Liệt kê");
            }                    
            return Json(param, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Lấy danh sách các triệu chứng trong cơ sở dữ liệu và trả về dạng JSon
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ListAllPaging( int? page = 1, int? rows = 10 )
        {
            int pageIndex = page.HasValue ? (int)page : 1;
            int pageSize = rows.HasValue ? (int)rows : 10;          
            int total = 0;           
            var param = new MeasureParamDao().ListAllPaging(out total, pageIndex, pageSize);
            if (param ==null)
            {
                return NotifyError("Liệt kê");
            }
            return Json(new { total = total, rows = param });
        }
        /// <summary>
        /// Thêm một đơn vị mới
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Create(TM_MeasureParam param)
        {
            param.CreatedDate = DateTime.Now;
            int result = new MeasureParamDao().Create(param);
            if (result >0)
            {
                return Json(param);
            }
            else
            {
                return NotifyError("Thêm");
            }

        }
        /// <summary>
        /// Cập nhật chi tiết đơn vị
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update([Bind(Include = "Id,CodeName,Description,Unit,Status")] TM_MeasureParam param)
        {
            bool result = new MeasureParamDao().Update(param);
            if (result)
            {
                return Json(new
                {
                    isNewRecord = false,
                    Id = param.Id,
                    CodeName = param.CodeName,
                    Description = param.Description,
                    Unit = param.Unit,
                    Status = param.Status
                });
            }
            else
            {
                return NotifyError("Sửa");
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
            bool result = new MeasureParamDao().Delete(id);
            
            if (result)
            {
                return Json(new { success = true });
            }
            else
            {
                return NotifyError("Xóa");
            }
        }
    }
}