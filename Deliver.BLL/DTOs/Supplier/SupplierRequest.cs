namespace Deliver.BLL.DTOs.Supplier;
public record SupplierRequest
(
    string OwnerName,
    string ShopName,
    string PhoneNumber,
    string Address,
    string ShopDescription,
    IFormFile? Image,
    TimeSpan? OpeningTime,
    TimeSpan ? ClosingTime,
    int ApplicationUserId,
    int SubCategoryId
);
