using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tm.Data.ViewModels;
using Tm.Data.Models;

namespace Tm.Data.Functions
{
    public class ReceptionDao:CommonDao
    {
        // Create new address, assign address to new user, create new order, assign order to doctor
        public ReceptionRegisterSp_Result CreateNewOrder(SpReceptionRegisterViewModel model)
        {
            return db.ReceptionRegisterSp(model.UserId, model.Address, model.WardId, model.Syptom, model.DoctorId).FirstOrDefault();
        }
    }
}
