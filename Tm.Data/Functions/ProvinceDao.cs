using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tm.Data.Models;

namespace Tm.Data.Functions
{
    public class ProvinceDao:CommonDao
    {
        public List<Province> ListAll()
        {
            var param = db.Provinces
                                .Select(x => new Province()
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    TelephoneCode = x.TelephoneCode,
                                    Type = x.Type,
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
        public IEnumerable<Province> Search(string term)
        {
            if (term == null)
            {
                return db.Provinces.Select(d => new { d.Id, d.Name, d.Type, d.SortOrder, d.IsPublished, d.IsDeleted })
                                 .OrderBy(d => d.Id)
                                
                                 .AsEnumerable().Select(x => new Province()
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
                return db.Provinces.Select(d => new { d.Id, d.Name, d.Type, d.SortOrder, d.IsPublished, d.IsDeleted })
                                .OrderBy(d => d.Id)
                                .Where(d => d.Name.Contains(term))
                                .AsEnumerable().Select(x => new Province()
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
