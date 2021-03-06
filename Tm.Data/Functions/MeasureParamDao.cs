﻿using System;
using System.Collections.Generic;
using System.Linq;
using Tm.Data.Models;
using Tm.Data.ViewModels;

namespace Tm.Data.Functions
{
    public class MeasureParamDao:CommonDao
    {  
        //Find MeasureParam id by CodeName
        public int FindIdByCodeName(string codeName)
        {
            return db.TM_MeasureParam.Where(p => p.CodeName.Equals(codeName)).FirstOrDefault().Id;
        }
        // List all params 
        public List<MeasureParamDetail> ListAll()
        {
            var param = db.TM_MeasureParam
                                .Select(x=>new MeasureParamDetail() {
                                        Id = x.Id,
                                        CodeName = x.CodeName,
                                        Description = x.Description,
                                        Unit = x.Unit,
                                        Status = x.Status,
                                        Type = x.Type,
                                        TypeName = x.TM_ParamType.TypeName
                                    })
                                .OrderBy(d => d.Type)
                                .Where(d => d.Status == true)
                                .ToList();
            return param;
        }
        // List all params by type 
        public List<MeasureParamDetail> ListAll(int type)
        {
            var param = db.TM_MeasureParam
                                .Select(x => new MeasureParamDetail()
                                {
                                    Id = x.Id,
                                    CodeName = x.CodeName,
                                    Description = x.Description,
                                    Unit = x.Unit,
                                    Status = x.Status,
                                    Type = x.Type,
                                    TypeName = x.TM_ParamType.TypeName
                                })
                                .OrderBy(d => d.Id)
                                .Where(d => d.Type == type)
                                .ToList();
            return param;
        }
        // Create a new param
        public int Create(TM_MeasureParam entity)
        {
            entity.CreatedDate = DateTime.Now;
            db.TM_MeasureParam.Add(entity);         
            db.SaveChanges();
            return entity.Id;
        }
        // Update a param
        public bool Update(TM_MeasureParam entity)
        {
            try
            {
                var param = db.TM_MeasureParam.Find(entity.Id);                          
                if (!string.IsNullOrEmpty(entity.CodeName))
                {
                    param.CodeName = entity.CodeName;
                }
                if (entity.Type !=null)
                {
                    param.Type = entity.Type;
                }
                if (!string.IsNullOrEmpty(entity.Unit))
                {
                    param.Unit = entity.Unit;
                }
                if (!string.IsNullOrEmpty(entity.Description))
                {
                    param.Description = entity.Description;
                }
                if (entity.Status!=null)
                {
                    param.Status = entity.Status;
                }          
                db.SaveChanges();
                return true;
            }
            catch (Exception )
            {
                //logging
                return false;
            }
        }

        // Change param status
        public bool ChangeStatus(int Id)
        {
            var param = db.TM_MeasureParam.Find(Id);
            param.Status = !param.Status;
            db.SaveChanges();
            return true;
        }

        // Delete a param
        public bool Delete(int Id)
        {
            try
            {
                var param = db.TM_MeasureParam.Find(Id);
                db.TM_MeasureParam.Remove(param);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // List all params and paging
        public IEnumerable<MeasureParamDetail> ListAllPaging(out int total,int pageIndex,int pageSize)
        {                
            int skip = (pageSize * (pageIndex - 1));
            total = db.TM_MeasureParam.Count();
            if (skip > total)
            {
                return null;
            }
            return db.TM_MeasureParam.Select(d => new { d.Id, d.CodeName, d.Description, d.Unit,d.Type,d.TM_ParamType.TypeName, d.Status })
                                 .OrderBy(d => d.Type).ThenBy(n=>n.CodeName)
                                 .Skip(skip)
                                 .Take(pageSize)
                                 .AsEnumerable().Select(x=>new MeasureParamDetail() {
                                     Id = x.Id,
                                     CodeName = x.CodeName,
                                     Description = x.Description,
                                     Unit = x.Unit,
                                     Type = x.Type,
                                     TypeName = x.TypeName,
                                     Status = x.Status
                                 });
        }
    }
}
