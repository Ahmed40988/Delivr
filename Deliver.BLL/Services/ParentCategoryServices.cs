using Deliver.BLL.DTOs.Category.ParentCategory;
using Deliver.Dal.Abstractions.Errors;
using Deliver.Dal.Data;
using Deliver.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Deliver.BLL.Services;

public class ParentCategoryServices(ApplicationDbContext context) : IParentCategoryServices
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result> CreateAsync(ParentCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var categoryExist = await _context.parentCategories
            .AnyAsync(x => x.Name == request.Name, cancellationToken);

        if (categoryExist)
        {
            return Result.Failure(CategoryError.ParentCategoryDuplicatedName);
        }

        var category = request.Adapt<ParentCategory>();

        if (request.Photo != null)
        {
            var newPhotoUrl = FileHelper.FileHelper.UploadFile(request.Photo, "category");
            category.Icon = newPhotoUrl;
        }

        await _context.parentCategories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<ParentCategoryResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _context.parentCategories.ToListAsync(cancellationToken);

        if (categories == null || !categories.Any())
            return Result.Failure<IEnumerable<ParentCategoryResponse>>(CategoryError.ParentCategoryNotFound);

        var categoriesResponse = categories.Adapt<IEnumerable<ParentCategoryResponse>>();
        return Result.Success(categoriesResponse);
    }

    public async Task<Result<ParentCategoryResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _context.parentCategories.FindAsync(new object?[] { id }, cancellationToken);

        if (category == null)
            return Result.Failure<ParentCategoryResponse>(CategoryError.ParentCategoryNotFound);

        var categoryResponse = category.Adapt<ParentCategoryResponse>();
        return Result.Success(categoryResponse);
    }

    public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _context.parentCategories.FindAsync(new object?[] { id }, cancellationToken);

        if (category == null)
            return Result.Failure<bool>(CategoryError.ParentCategoryNotFound);

        _context.parentCategories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(true);
    }

    public async Task<Result<bool>> UpdateAsync(int id, ParentCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var category = await _context.parentCategories.FindAsync(new object?[] { id }, cancellationToken);

        if (category == null)
            return Result.Failure<bool>(CategoryError.ParentCategoryNotFound);

        var categoryExist = await _context.parentCategories
            .AnyAsync(x => x.Name == request.Name && x.Id != id, cancellationToken);

        if (categoryExist)
            return Result.Failure<bool>(CategoryError.ParentCategoryDuplicatedName);

        category = request.Adapt(category);

        _context.parentCategories.Update(category);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(true);
    }
}
