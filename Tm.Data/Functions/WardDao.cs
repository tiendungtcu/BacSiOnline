using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tm.Data.ViewModels;

namespace Tm.Data.Functions
{
    public class WardDao:CommonDao
    {
        public List<WardDetail> ListAll()
        {
            var param = db.Wards
                                .Select(x => new WardDetail()
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Type = x.Type,
                                    DistrictID = x.DistrictID,
                                    DistrictName = x.District.Type + " " + x.District.Name,
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
        public IEnumerable<WardDetail> Search(int disid, string term)
        {
            if (term == null)
            {
                return db.Wards.Select(d => new { d.Id, d.Name, d.Type, d.DistrictID, d.SortOrder, d.IsPublished, d.IsDeleted })
                                 .OrderBy(d => d.SortOrder)
                                 .Where(d => d.DistrictID == disid)
                                 .AsEnumerable().Select(x => new WardDetail()
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
                return db.Wards.Select(d => new { d.Id, d.Name, d.Type, d.DistrictID, d.SortOrder, d.IsPublished, d.IsDeleted })
                                .OrderBy(d => d.SortOrder)
                                .Where(d => d.Name.Contains(term) && d.DistrictID == disid)
                                .AsEnumerable().Select(x => new WardDetail()
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
