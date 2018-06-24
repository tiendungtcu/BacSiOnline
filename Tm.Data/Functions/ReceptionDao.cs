using System.Linq;
using Tm.Data.Models;
using Tm.Data.ViewModels.Reception;

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
