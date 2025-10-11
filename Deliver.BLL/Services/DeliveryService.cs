using Deliver.BLL.DTOs.Delivery;
using Microsoft.Extensions.Logging;
namespace Deliver.BLL.Services
{
    public class DeliveryService(IDeliveryRepository deliveryRepository,UserManager<ApplicationUser> userManager, ILogger<DeliveryService> logger) : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository = deliveryRepository;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILogger<DeliveryService> _logger = logger;

        public async Task<Result> ChooseVehicleTypeAsync(int userId, VehicleTypeenum vehicleType)
        {
            var delivery = await _deliveryRepository.getDeliveryAsync(userId);
            if (delivery == null)
                return Result.Failure(UserErrors.DeliveryNotFound);

            var vech = await _deliveryRepository.GetVehicleTypeByEnumAsync(vehicleType);
            if (vech == null)
                return Result.Failure(UserErrors.invalidVehicle);

            bool alreadyExists = await _deliveryRepository.UserHasVehicleTypeAsync(userId, vech.Id);
            if (alreadyExists)
                return Result.Failure(UserErrors.DuplicatedVehicle);


            delivery.vehicle_type_id = vech.Id;
            await _deliveryRepository.updateDeliveryAsync(delivery);
            return Result.Success();

        }

        public async Task<Result> CompleteDeliveryProfileasync(int userid, CompleteProfileDeliveryDTO request)
        {
            var delivery = await _deliveryRepository.getDeliveryAsync(userid);
            if (delivery == null)
                return Result.Failure(UserErrors.DeliveryNotFound);

            if (!await _deliveryRepository.Checkemail(userid, request.Email))
                return Result.Failure(UserErrors.UserNotFound);

            string newPhotoUrl = delivery.PhotoUrl;

            if (request.Photo != null)
            {
                try
                {
                    newPhotoUrl = FileHelper.FileHelper.UploadFile(request.Photo, "Delivery");
                    _logger.LogInformation("Uploaded new photo for user {UserId}: {PhotoUrl}", userid, newPhotoUrl);

                    if (!string.IsNullOrEmpty(delivery.PhotoUrl))
                    {
                        FileHelper.FileHelper.DeleteFile(delivery.PhotoUrl, "Delivery");
                        _logger.LogInformation("Deleted old photo for user {UserId}", userid);
                    }
                }
                catch (Exception fileEx)
                {
                    _logger.LogError(fileEx, "Error handling photo upload for user {UserId}", userid);
                  
                }
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
