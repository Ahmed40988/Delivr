using Microsoft.AspNetCore.Http;

namespace Deliver.Dal.Abstractions.Errors
{
    public static class SupplierError
    {
        public static readonly Abstractions.Error SupplierNotFound =
            new("Supplier.NotFound", "The specified supplier was not found.", StatusCodes.Status404NotFound);

        public static readonly Abstractions.Error SupplierDuplicatedShopName =
            new("Supplier.DuplicatedShopName", "A supplier with this shop name already exists.", StatusCodes.Status409Conflict);

        public static readonly Abstractions.Error SupplierDuplicatedPhone =
            new("Supplier.DuplicatedPhone", "A supplier with this phone number already exists.", StatusCodes.Status409Conflict);

        public static readonly Abstractions.Error SupplierInvalidData =
            new("Supplier.InvalidData", "Required supplier fields are missing or invalid.", StatusCodes.Status400BadRequest);

        public static readonly Abstractions.Error SupplierUnauthorized =
            new("Supplier.Unauthorized", "You are not authorized to access this supplier resource.", StatusCodes.Status401Unauthorized);

        public static readonly Abstractions.Error SupplierHasActiveOrders =
            new("Supplier.HasActiveOrders", "Cannot delete supplier because there are active linked orders.", StatusCodes.Status400BadRequest);
    }
}
