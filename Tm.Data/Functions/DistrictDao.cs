using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tm.Data.Models;
using Tm.Data.ViewModels;

namespace Tm.Data.Functions
{
    public class DistrictDao:CommonDao
    {
        public List<DistrictDetail> ListAll()
        {
            var param = db.Districts
                                .Select(x => new DistrictDetail()
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Type = x.Type,
                                    ProvinceId = x.ProvinceId,
                                    ProvinceName = x.Province.Type+" "+x.Province.Name,
                                    SortOrder = x.SortOrder,
                                    IsDeleted = x.IsDeleted,
                                    IsPublished = x.IsPublished
                                })
                                .OrderBy(d => d.SortOrder)
                                .Where(d => d.IsPublished == true)
                                .ToList();
            return param;
        }
        // Search Province base on keyword
        public IEnumerable<DistrictDetail> Search(int proid,string term)
        {
            if (term == null)
            {
                return db.Districts.Select(d => new { d.Id, d.Name, d.Type, d.ProvinceId, d.SortOrder, d.IsPublished, d.IsDeleted })
                                 .OrderBy(d => d.SortOrder)
                                 .Where(d=>d.ProvinceId==proid)
                                 .AsEnumerable().Select(x => new DistrictDetail()
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     SortOrder = x.SortOrder,
                                     Type = x.Type,
                                     IsDeleted = x.IsDeleted,
                                     IsPublished = x.IsPublished
                                 });
            }
            else
            {
                return db.Districts.Select(d => new { d.Id, d.Name, d.Type, d.ProvinceId, d.SortOrder, d.IsPublished, d.IsDeleted })
                                .OrderBy(d => d.SortOrder)
                                .Where(d => d.Name.Contains(term)&& d.ProvinceId == proid)
                                .AsEnumerable().Select(x => new DistrictDetail()
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    SortOrder = x.SortOrder,
                                    Type = x.Type,
                                    IsDeleted = x.IsDeleted,
                                    IsPublished = x.IsPublished
                                });
            }
        }
    }
}
