using Deliver.Entities.Entities;
using Deliver.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.Entities.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<bool>Checkemail(int userid,string email);
        Task<VehicleType?> GetVehicleTypeByEnumAsync(VehicleTypeenum vehicleType);
        Task<bool> UserHasVehicleTypeAsync(int userId, int vehicleTypeId);
        Task AddDeliveryAsync(Delivery delivery);
        Task<Delivery> getDeliveryAsync(int userid);
        Task updateDeliveryAsync(Delivery delivery);


    }
}
