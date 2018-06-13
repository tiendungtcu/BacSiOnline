using System;
using System.Collections.Generic;
using System.Linq;
using Tm.Data.Models;
using Tm.Data.ViewModels;

namespace Tm.Data.Functions
{
    public class NotifyTypeDao:CommonDao
    {       
        public List<NotifyTypeDto> ListAll()
        {
            var param = db.TM_NotifyType
                                .Select(x => new NotifyTypeDto
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Status = x.Status
                                })
                                .OrderBy(d => d.Id)
                                .Where(d => d.Status == true)
                                .ToList();
            return param;
        }
        // Create
        public byte Create(TM_NotifyType entity)
        {
            db.TM_NotifyType.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        // Update 
        public bool Update(TM_NotifyType entity)
        {
            try
            {
                var result = db.TM_NotifyType.Find(entity.Id);
                result.Name = entity.Name;
                result.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {             
                return false;
            }
        }
        public bool Delete(int Id)
        {
            try
            {
                var entity = db.TM_NotifyType.Find(Id);
                db.TM_NotifyType.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<NotifyTypeDto> ListAllPaging(out int total, int pageIndex, int pageSize)
        {
            int skip = (pageSize * (pageIndex - 1));
            total = db.TM_NotifyType.Count();
            if (skip > total)
            {
                return null;
            }
            return db.TM_NotifyType.Select(d => new { d.Id, d.Name, d.Status })
                                 .OrderBy(d => d.Id)
                                 .Skip(skip)
                                 .Take(pageSize)
                                 .AsEnumerable().Select(x => new NotifyTypeDto()
                                 {
                                     Id = x.Id,
                                     Name = x.Name,                                     
                                     Status = x.Status
                                 });
        }
    }
}
