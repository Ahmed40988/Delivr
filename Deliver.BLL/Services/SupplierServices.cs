
using Deliver.BLL.DTOs.Supplier;
using Deliver.Entities.Entities;
using Microsoft.AspNetCore.Hosting;

namespace Deliver.BLL.Services;

public class SupplierServices(UserManager<ApplicationUser>userManager , IUnitOfWork unitOfWork , IWebHostEnvironment env) : ISupplierServices
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IWebHostEnvironment _env = env;

    public async Task<Result> CreateSupplierAsync(SupplierRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.ApplicationUserId.ToString());
        if (user == null)
            return Result.Failure(UserErrors.UserNotFound);
        var subCategory = await _unitOfWork.SubCategories.GetByIdAsync(request.SubCategoryId);
        if(subCategory == null)
            return  Result.Failure(CategoryError.SubCategoryNotFound);
    
        var supplier = request.Adapt<Supplier>();
        string newPhotoUrl = supplier.Photo;
        if (request.Image != null)
        {
            newPhotoUrl = FileHelper.FileHelper.UploadFile(request.Image, "Supplier",_env);
            if (!string.IsNullOrEmpty(supplier.Photo))
                FileHelper.FileHelper.DeleteFile(supplier.Photo, "Supplier",_env);
        }
        user.FirstName = request.OwnerName;
        user.PhoneNumber = request.PhoneNumber;
        supplier.Photo = newPhotoUrl;

        await _userManager.UpdateAsync(user);
        await  _unitOfWork.Suppliers.AddAsync(supplier);
        await _unitOfWork.SaveAsync();
        return Result.Success(supplier);
    } 

    public Task<bool> DeleteSupplierAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<SupplierResponse>> GetSupplierByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateSupplierAsync(int id, SupplierRequest request)
    {
        throw new NotImplementedException();
    }
}
