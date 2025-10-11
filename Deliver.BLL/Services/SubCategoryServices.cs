using Deliver.BLL.DTOs.Category.SubCategory;
using Deliver.Dal.Abstractions.Errors;
using Deliver.Dal.Data;
using Deliver.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Deliver.BLL.Services;

public class SubCategoryServices(ApplicationDbContext context) : ISubCategoryServices
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result> CreateAsync(SubCategoryRequest request)
    {
        var subCategoryExist = await _context.subCategories
            .AnyAsync(x => x.Name == request.Name && x.ParentCategoryId == request.ParentCategoryId);

        if (subCategoryExist)
            return Result.Failure(CategoryError.SubCategoryDuplicatedName);

        var subCategory = request.Adapt<SubCategory>();

        if (request.Icon != null)
        {
            var newPhotoUrl = FileHelper.FileHelper.UploadFile(request.Icon, "subcategory");
            subCategory.Icon = newPhotoUrl;
        }

        await _context.subCategories.AddAsync(subCategory);
        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<IEnumerable<SubCategoryResponse>>> GetAllAsync()
    {
        var subCategories = await _context.subCategories.ToListAsync();

        if (subCategories == null || !subCategories.Any())
            return Result.Failure<IEnumerable<SubCategoryResponse>>(CategoryError.SubCategoryNotFound);

        var response = subCategories.Adapt<IEnumerable<SubCategoryResponse>>();
        return Result.Success(response);
    }

    public async Task<Result<SubCategoryResponse>> GetByIdAsync(int id)
    {
        var subCategory = await _context.subCategories.FindAsync(id);

        if (subCategory == null)
            return Result.Failure<SubCategoryResponse>(CategoryError.SubCategoryNotFound);

        var response = subCategory.Adapt<SubCategoryResponse>();
        return Result.Success(response);
    }

    public async Task<Result<bool>> UpdateAsync(int id, SubCategoryRequest request)
    {
        var subCategory = await _context.subCategories.FindAsync(id);

        if (subCategory == null)
            return Result.Failure<bool>(CategoryError.SubCategoryNotFound);

        var subCategoryExist = await _context.subCategories
            .AnyAsync(x => x.Name == request.Name && x.Id != id && x.ParentCategoryId == request.ParentCategoryId);

        if (subCategoryExist)
            return Result.Failure<bool>(CategoryError.SubCategoryDuplicatedName);

        subCategory = request.Adapt(subCategory);

        if (request.Icon != null)
        {
            if (!string.IsNullOrEmpty(subCategory.Icon))
                FileHelper.FileHelper.DeleteFile(subCategory.Icon, "subcategory");

            var newPhotoUrl =  FileHelper.FileHelper.UploadFile(request.Icon, "subcategory");
            subCategory.Icon = newPhotoUrl;
        }

        _context.subCategories.Update(subCategory);
        await _context.SaveChangesAsync();

        return Result.Success(true);
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var subCategory = await _context.subCategories.FindAsync(id);

        if (subCategory == null)
            return Result.Failure<bool>(CategoryError.SubCategoryNotFound);

        if (!string.IsNullOrEmpty(subCategory.Icon))
            FileHelper.FileHelper.DeleteFile(subCategory.Icon, "subcategory");

        _context.subCategories.Remove(subCategory);
        await _context.SaveChangesAsync();

        return Result.Success(true);
    }
}
