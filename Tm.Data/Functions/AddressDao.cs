using System;
using Tm.Data.Models;

namespace Tm.Data.Functions
{
    public class AddressDao:CommonDao
    {
        /*
        public int Create(int WardId, string address)
        {
            TM_Address entity = new TM_Address();
            entity.WardId = WardId;
            entity.Address = address;
            db.TM_Address.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        */
        public int Create(TM_Address entity)
        {
            db.TM_Address.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        public int AssignUserToAddress(int userId,int addressId)
        {
            try
            {
                UserAddress entity = new UserAddress();
                entity.UserId = userId;
                entity.AddressId = addressId;
                entity.StartDate = DateTime.Now;
                db.UserAddresses.Add(entity);
                db.SaveChanges();
                return entity.Id;
            }
            catch (Exception)
            {

                return -1;
            }
            
        }
        public int AssignUserToAddress(UserAddress entity)
        {
            try
            {
                db.UserAddresses.Add(entity);
                db.SaveChanges();
                return entity.Id;
            }
            catch (Exception)
            {

                return -1;
            }

        }
    }
}
