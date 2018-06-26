using System;
using System.Collections.Generic;
using System.Linq;
using Tm.Data.Models;
using Tm.Data.ViewModels;

namespace Tm.Data.Functions
{
    public class NotificationDao:CommonDao
    {   
        // Find link for a notification
        public int FindLink(int notId)
        {
            var link= db.TM_Notification.Find(notId).Link;
            return link.Value;
        }
        // Change a notification to viewed
        public bool HasBeenViewed(int id)
        {
            try
            {
                var notify = db.TM_Notification.Find(id);
                notify.Status = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
        // Find a notification by its Id
        public TM_Notification Find(int id)
        {
            return db.TM_Notification.Find(id);
        }

        // Get All notification of a user
        public IList<NotificationDetail> ListByUser(int userId)
        {
           return  db.TM_Notification
                                .Select(x => new NotificationDetail()
                                {
                                    Id = x.Id,
                                    Title = x.Title,
                                    Contents = x.Contents,
                                    ReceiverId = x.ReceiverId,
                                    Receiver = x.TM_Users.UserName,
                                    Type = x.Type,
                                    TypeName = x.TM_NotifyType.Name,
                                    Link = x.Link,
                                    CreatedDate = x.CreatedDate,
                                    Status = x.Status
                                })                               
                                .Where(d => d.ReceiverId == userId && d.Status == false)
                                .OrderBy(d => d.Id)
                                .ToList();
        }
        // List all
        public List<NotificationDetail> ListAll()
        {
            return db.TM_Notification
                                .Select(x => new NotificationDetail()
                                {
                                    Id = x.Id,
                                    Title = x.Title,
                                    Contents = x.Contents,
                                    ReceiverId = x.ReceiverId,
                                    Receiver = x.TM_Users.UserName,
                                    Type = x.Type,
                                    TypeName = x.TM_NotifyType.Name,
                                    Link = x.Link,
                                    CreatedDate = x.CreatedDate,
                                    Status = x.Status
                                })
                                .OrderBy(d => d.Id)
                                .Where(d => d.Status == true)
                                .ToList();
        }
        // Create
        public long Create(TM_Notification entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = false;
            db.TM_Notification.Add(entity);           
            db.SaveChanges();
            return entity.Id;
        }
        // Update
        public bool Update(NotificationDetail entity)
        {
            try
            {
                var param = db.TM_Notification.Find(entity.Id);
                if (!string.IsNullOrEmpty(entity.Title))
                {
                    param.Title = entity.Title;
                }
                if (!string.IsNullOrEmpty(entity.Contents ))
                {
                    param.Contents = entity.Contents;
                }
                if ( entity.ReceiverId!=null)
                {
                    param.ReceiverId = entity.ReceiverId;
                }
                if (entity.Type!=null)
                {
                    param.Type = entity.Type;
                }
                if (entity.Link!=null)
                {
                    param.Link = entity.Link;
                }
                param.Status = entity.Status;
                       
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        // Delete
        public bool Delete(int Id)
        {
            try
            {
                var entity = db.TM_Notification.Find(Id);
                db.TM_Notification.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        // List Index
        public IEnumerable<NotificationDetail> ListAllPaging(out int total, int pageIndex, int pageSize)
        {
            int skip = (pageSize * (pageIndex - 1));
            total = db.TM_MeasureParam.Count();
            if (skip > total)
            {
                return null;
            }
            return db.TM_Notification.Select(d => new {
                                            d.Id,
                                            d.Title,
                                            d.Contents,
                                            d.Type,
                                            TypeName =d.TM_NotifyType.Name,
                                            ReceiverId = d.ReceiverId,
                                            Receiver=d.TM_Users.UserName,
                                            d.Link,
                                            d.CreatedDate,
                                            d.Status
                                 })
                                 .OrderBy(d => d.Id)
                                 .Skip(skip)
                                 .Take(pageSize)
                                 .AsEnumerable().Select(x => new NotificationDetail()
                                 {
                                     Id = x.Id,
                                     Title = x.Title,
                                     Contents = x.Contents,
                                     ReceiverId = x.ReceiverId,
                                     Receiver = x.Receiver,
                                     Type = x.Type,
                                     TypeName = x.TypeName,
                                     Link = x.Link,
                                     CreatedDate = x.CreatedDate,
                                     Status = x.Status
                                 });
        }

        // List Index
        public IEnumerable<NotificationDetail> ListAllPaging(out int total,string keyword,int type,int receiver, int pageIndex, int pageSize)
        {
            int skip = (pageSize * (pageIndex - 1));          
            var data = from m in db.TM_Notification
                       select m; 
            if (!String.IsNullOrEmpty(keyword))
            {
                data = data.Where(x => x.Contents.Contains(keyword) || x.Title.Contains(keyword));
            }
            if (type>0)
            {
                data = data.Where(x => x.Type == type);
            }
            if (receiver>0)
            {
                data = data.Where(x => x.ReceiverId == receiver);
            }
            total = data.Count();
            if (skip > total)
            {
                return null;
            }
            return data.Select(d => new {
                d.Id,
                d.Title,
                d.Contents,
                d.Type,
                TypeName = d.TM_NotifyType.Name,
                ReceiverId = d.ReceiverId,
                Receiver = d.TM_Users.UserName,
                d.Link,
                d.CreatedDate,
                d.Status
            })
                                 .OrderBy(d => d.Id)
                                 .Skip(skip)
                                 .Take(pageSize)
                                 .AsEnumerable().Select(x => new NotificationDetail()
                                 {
                                     Id = x.Id,
                                     Title = x.Title,
                                     Contents = x.Contents,
                                     ReceiverId = x.ReceiverId,
                                     Receiver = x.Receiver,
                                     Type = x.Type,
                                     TypeName = x.TypeName,
                                     Link = x.Link,
                                     CreatedDate = x.CreatedDate,
                                     Status = x.Status
                                 });
        }
    }
}
