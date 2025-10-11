using Deliver.BLL.DTOs.Delivery;
namespace Deliver.BLL.Services
{
    public class DeliveryService(IDeliveryRepository deliveryRepository,UserManager<ApplicationUser> userManager) : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository = deliveryRepository;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<Result> ChooseVehicleTypeAsync(int userId, VehicleTypeenum vehicleType)
        {
            var vech = await _deliveryRepository.GetVehicleTypeByEnumAsync(vehicleType);
            if (vech == null)
                return Result.Failure(UserErrors.invalidVehicle);

            bool alreadyExists = await _deliveryRepository.UserHasVehicleTypeAsync(userId, vech.Id);
            if (alreadyExists)
                return Result.Failure(UserErrors.DuplicatedVehicle);


            var delivery =await _deliveryRepository.getDeliveryAsync(userId);
            delivery.vehicle_type_id=vech.Id;
            await _deliveryRepository.updateDeliveryAsync(delivery);
            return Result.Success();
        }

        public async Task<Result> CompleteDeliveryProfileasync(int userid, CompleteProfileDeliveryDTO request)
        {
            if (!await _deliveryRepository.Checkemail(userid, request.Email))
                return Result.Failure(UserErrors.UserNotFound);

            var delivery=await _deliveryRepository.getDeliveryAsync(userid);

            string newPhotoUrl = delivery.PhotoUrl;
            if (request.Photo != null)
            {
                newPhotoUrl = FileHelper.FileHelper.UploadFile(request.Photo, "Delivery");
                if (!string.IsNullOrEmpty(delivery.PhotoUrl))
                    FileHelper.FileHelper.DeleteFile(delivery.PhotoUrl, "Delivery");
            }

            delivery.ApplicationUser.FirstName = request.FirstName;
            delivery.ApplicationUser.LastName = request.LastName;
            delivery.ApplicationUser.PhoneNumber = request.Phone;
            delivery.city = request.city;
            delivery.PhotoUrl = newPhotoUrl;
            await _deliveryRepository.updateDeliveryAsync(delivery);
            return Result.Success();
        }
    }
}
