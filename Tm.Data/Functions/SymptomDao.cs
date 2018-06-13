using System;
using System.Collections.Generic;
using System.Linq;
using Tm.Data.Models;

namespace Tm.Data.Functions
{
    public class SymptomDao:CommonDao
    {
        
        public IEnumerable<TM_Symptom> ListAll()
        {
            var param = db.TM_Symptom
                    .Select(x => new TM_Symptom()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Status = x.Status
                    })
                    .OrderBy(d => d.Id)
                    .Where(d => d.Status == true);
                    //.ToList();
            return param;
        }
        public int Create(TM_Symptom symptom)
        {
            symptom.CreatedDate = DateTime.Now;
            db.TM_Symptom.Add(symptom);
            db.SaveChanges();
            return symptom.Id;
        }

        public bool Update(TM_Symptom entity)
        {
            try
            {
                var symptom = db.TM_Symptom.Find(entity.Id);
                symptom.Name = entity.Name;
                symptom.Description = entity.Description;
                db.SaveChanges();
                return true;
            }
            catch (Exception )
            {
                //logging
                return false;
            }
        }

        public bool ChangeStatus(int Id)
        {
            var symptom = db.TM_Symptom.Find(Id);
            symptom.Status = !symptom.Status;
            db.SaveChanges();
            return true;
        }

        public bool Delete(int Id)
        {
            try
            {
                var symptom = db.TM_Symptom.Find(Id);
                db.TM_Symptom.Remove(symptom);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public TM_Symptom Find(int Id)
        {
            return db.TM_Symptom.Find(Id);
        }
        public IEnumerable<TM_Symptom> ListAllPaging(out int total, int pageIndex, int pageSize)
        {
            int skip = (pageSize * (pageIndex - 1));
            total = db.TM_Symptom.Count();
            if (skip > total)
            {
                return null;
            }
            return db.TM_Symptom.Select(d => new { d.Id, d.Name, d.Description, d.CreatedDate, d.Status })
                                 .OrderBy(d => d.Id)
                                 .Skip(skip)
                                 .Take(pageSize)
                                 .AsEnumerable().Select(x => new TM_Symptom()
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     Description = x.Description,
                                     CreatedDate = x.CreatedDate,
                                     Status = x.Status
                                 });
        }

        public IEnumerable<TM_Symptom> Search(string term)
        {
            if (term == null)
            {
                return db.TM_Symptom.Select(d => new { d.Id, d.Name, d.Description, d.CreatedDate, d.Status })
                                 .OrderBy(d => d.Id)
                                 .AsEnumerable().Select(x => new TM_Symptom()
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     Description = x.Description,
                                     CreatedDate = x.CreatedDate,
                                     Status = x.Status
                                 });
            }
            else
            {
                return db.TM_Symptom.Select(d => new { d.Id, d.Name, d.Description, d.CreatedDate, d.Status })
                                 .OrderBy(d => d.Id)
                                 .Where(d => d.Description.Contains(term))
                                 .AsEnumerable().Select(x => new TM_Symptom()
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     Description = x.Description,
                                     CreatedDate = x.CreatedDate,
                                     Status = x.Status
                                 });
            }
        }
    }
}
