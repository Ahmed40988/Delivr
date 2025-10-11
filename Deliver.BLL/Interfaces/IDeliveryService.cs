using Deliver.BLL.DTOs.Delivery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.BLL.Interfaces
{
    public interface IDeliveryService
    {
        Task<Result> ChooseVehicleTypeAsync(int userId, VehicleTypeenum vehicleType);
        Task<Result> CompleteDeliveryProfileasync(int userid,CompleteProfileDeliveryDTO request);

    }
}
