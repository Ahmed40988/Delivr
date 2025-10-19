using Deliver.BLL.DTOs.Supplier;
using Deliver.Entities.Entities;
using Microsoft.AspNetCore.Hosting;
using Deliver.Dal.Abstractions.Errors;

namespace Deliver.BLL.Services;

public class SupplierServices(
    UserManager<ApplicationUser> userManager,
    IUnitOfWork unitOfWork,
    IWebHostEnvironment env) : ISupplierServices
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IWebHostEnvironment _env = env;

    public async Task<Result<Supplier>> CreateSupplierAsync(SupplierRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.ApplicationUserId.ToString());
        if (user == null)
            return Result.Failure<Supplier>(UserErrors.UserNotFound);

        var subCategory = await _unitOfWork.SubCategories.GetByIdAsync(request.SubCategoryId);
        if (subCategory == null)
        {
            return Result.Failure<Supplier>(new Error(
                "SubCategory.NotFound",
                $"SubCategory with ID {request.SubCategoryId} does not exist.",
                StatusCodes.Status404NotFound
            ));
        }

        var existingSupplierByUser = await _unitOfWork.Suppliers
            .FindAsync(s => s.ApplicationUserId == request.ApplicationUserId);
        if (existingSupplierByUser != null)
            return Result.Failure<Supplier>(SupplierError.SupplierAlreadyExists);

        var existingSupplierByShopName = await _unitOfWork.Suppliers
            .FindAsync(s => s.ShopName.ToLower() == request.ShopName.ToLower());
        if (existingSupplierByShopName != null)
            return Result.Failure<Supplier>(SupplierError.SupplierDuplicatedShopName);

        var existingSupplierByPhone = await _unitOfWork.Suppliers
            .FindAsync(s => s.PhoneNumber == request.PhoneNumber);
        if (existingSupplierByPhone != null)
            return Result.Failure<Supplier>(SupplierError.SupplierDuplicatedPhone);

        string? newPhotoUrl = null;

        try
        {
            var supplier = request.Adapt<Supplier>();

            supplier.SubCategoryId = request.SubCategoryId;

            if (request.Image != null)
            {
                newPhotoUrl = FileHelper.FileHelper.UploadFile(request.Image, "Supplier", _env);
                supplier.Photo = newPhotoUrl;
            }

            user.FirstName = request.OwnerName;
            user.PhoneNumber = request.PhoneNumber;
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                if (!string.IsNullOrEmpty(newPhotoUrl))
                    FileHelper.FileHelper.DeleteFile(newPhotoUrl, "Supplier", _env);

                var errors = string.Join("; ", updateResult.Errors.Select(e => e.Description));
                return Result.Failure<Supplier>(new Error(
                    "User.UpdateFailed",
                    $"Failed to update user information: {errors}",
                    StatusCodes.Status400BadRequest
                ));
            }

            await _unitOfWork.Suppliers.AddAsync(supplier);
            await _unitOfWork.SaveAsync();

            return Result.Success(supplier);
        }
        catch (Exception ex)
        {
            if (!string.IsNullOrEmpty(newPhotoUrl))
                FileHelper.FileHelper.DeleteFile(newPhotoUrl, "Supplier", _env);

            return Result.Failure<Supplier>(new Error(
                "Supplier.CreationFailed",
                $"Failed to create supplier: {ex.Message}",
                StatusCodes.Status500InternalServerError
            ));
        }
    }

    public async Task<Result> DeleteSupplierAsync(int id)
    {
        var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
        if (supplier == null)
            return Result.Failure(SupplierError.SupplierNotFound);

       

        try
        {
            if (!string.IsNullOrEmpty(supplier.Photo))
                FileHelper.FileHelper.DeleteFile(supplier.Photo, "Supplier", _env);

            _unitOfWork.Suppliers.Delete(supplier);
            await _unitOfWork.SaveAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error(
                "Supplier.DeleteFailed",
                $"Failed to delete supplier: {ex.Message}",
                StatusCodes.Status500InternalServerError
            ));
        }
    }

    public async Task<Result<SupplierResponse>> GetSupplierByIdAsync(int id)
    {
        var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
        if (supplier == null)
            return Result.Failure<SupplierResponse>(SupplierError.SupplierNotFound);

        var response = supplier.Adapt<SupplierResponse>();
        return Result.Success(response);
    }

    public async Task<Result<bool>> UpdateSupplierAsync(int id, SupplierRequest request)
    {
        var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
        if (supplier == null)
            return Result.Failure<bool>(SupplierError.SupplierNotFound);

        var subCategory = await _unitOfWork.SubCategories.GetByIdAsync(request.SubCategoryId);
        if (subCategory == null)
            return Result.Failure<bool>(CategoryError.SubCategoryNotFound);

        var user = await _userManager.FindByIdAsync(request.ApplicationUserId.ToString());
        if (user == null)
            return Result.Failure<bool>(UserErrors.UserNotFound);

        var existingSupplierByShopName = await _unitOfWork.Suppliers
            .FindAsync(s => s.ShopName.ToLower() == request.ShopName.ToLower() && s.Id != id);
        if (existingSupplierByShopName != null)
            return Result.Failure<bool>(SupplierError.SupplierDuplicatedShopName);

        var existingSupplierByPhone = await _unitOfWork.Suppliers
            .FindAsync(s => s.PhoneNumber == request.PhoneNumber && s.Id != id);
        if (existingSupplierByPhone != null)
            return Result.Failure<bool>(SupplierError.SupplierDuplicatedPhone);

        string? oldPhotoUrl = supplier.Photo;
        string? newPhotoUrl = oldPhotoUrl;

        try
        {
            if (request.Image != null)
            {
                newPhotoUrl = FileHelper.FileHelper.UploadFile(request.Image, "Supplier", _env);
            }

            request.Adapt(supplier);
            supplier.Photo = newPhotoUrl;
            supplier.Id = id; 

            user.FirstName = request.OwnerName;
            user.PhoneNumber = request.PhoneNumber;
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                if (newPhotoUrl != oldPhotoUrl && !string.IsNullOrEmpty(newPhotoUrl))
                    FileHelper.FileHelper.DeleteFile(newPhotoUrl, "Supplier", _env);

                return Result.Failure<bool>(UserErrors.UpdateFailed);
            }

            _unitOfWork.Suppliers.Update(supplier);
            await _unitOfWork.SaveAsync();

            if (newPhotoUrl != oldPhotoUrl && !string.IsNullOrEmpty(oldPhotoUrl))
                FileHelper.FileHelper.DeleteFile(oldPhotoUrl, "Supplier", _env);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            if (newPhotoUrl != oldPhotoUrl && !string.IsNullOrEmpty(newPhotoUrl))
                FileHelper.FileHelper.DeleteFile(newPhotoUrl, "Supplier", _env);

            return Result.Failure<bool>(new Error(
                "Supplier.UpdateFailed",
                $"Failed to update supplier: {ex.Message}",
                StatusCodes.Status500InternalServerError
            ));
        }
    }
}