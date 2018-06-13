using System.Linq;
using System.Web.Mvc;
using Tm.Data.Models;
using Tm.Data.Functions;

namespace TM.Web.Areas.Quantri.Controllers
{
    public class SymptomController : QuantriBaseController
    {
        // 
        [AllowAnonymous]
        public ActionResult TabView()
        {
            return PartialView("SymptomPartial");
        }
        /// <summary>
        /// Lấy danh sách trieu chung nạp vào combobox
        /// </summary>
        /// <returns></returns>
        public JsonResult ListAll()
        {         
            var symptoms = new SymptomDao().ListAll();
            return Json(symptoms, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Lấy danh sách trieu chung nạp vào Select2
        /// </summary>
        /// <returns></returns>
        public JsonResult SymptomDropdown(string term,  string q,string _type="query")
        {    
            var symptoms = new SymptomDao().Search(term).Select(x => new { id = x.Id, text = x.Name, disabled = x.Status == true ? false : true });          
            return Json(new { results = symptoms }, JsonRequestBehavior.AllowGet);
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
            var result = new SymptomDao().ListAllPaging(out total, pageIndex, pageSize);
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
        public JsonResult Create(TM_Symptom symptom)
        {
            int SymptomId = new SymptomDao().Create(symptom);
            symptom.Id = SymptomId;
            if (SymptomId>0)
            {
                return Json(symptom);
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
        public JsonResult Update([Bind(Include = "Id,Name,Description,Status")] TM_Symptom symptom)
        {
            bool ok = new SymptomDao().Update(symptom);           
            if (ok)
            {
                return Json(new
                {
                    isNewRecord = false,
                    Id = symptom.Id,
                    Name = symptom.Name,
                    Description = symptom.Description,
                    Status = symptom.Status
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
            bool ok = new SymptomDao().Delete(id);
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