using Microsoft.AspNetCore.Http;
using System;

namespace Deliver.Dal.Abstractions.Errors;

public static class CategoryError
{
    public static readonly Abstractions.Error ParentCategoryNotFound =
        new("Category.ParentNotFound", "The specified parent category was not found.", StatusCodes.Status404NotFound);

    public static readonly Abstractions.Error ParentCategoryDuplicatedName =
        new("Category.ParentDuplicatedName", "A parent category with this name already exists.", StatusCodes.Status409Conflict);

    public static readonly Abstractions.Error ParentCategoryHasSubcategories =
        new("Category.ParentHasSubcategories", "Cannot delete parent category because it contains linked subcategories.", StatusCodes.Status400BadRequest);

    public static readonly Abstractions.Error SubCategoryNotFound =
        new("Category.SubNotFound", "The specified subcategory was not found.", StatusCodes.Status404NotFound);

    public static readonly Abstractions.Error SubCategoryDuplicatedName =
        new("Category.SubDuplicatedName", "A subcategory with this name already exists under this parent category.", StatusCodes.Status409Conflict);

    public static readonly Abstractions.Error SubCategoryInvalidParent =
        new("Category.SubInvalidParent", "Cannot link subcategory: the specified ParentCategoryId is invalid or not found.", StatusCodes.Status400BadRequest);

    public static readonly Abstractions.Error InvalidCategoryData =
        new("Category.InvalidData", "Required category fields are missing or invalid.", StatusCodes.Status400BadRequest);
}