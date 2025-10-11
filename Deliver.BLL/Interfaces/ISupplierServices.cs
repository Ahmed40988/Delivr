using Deliver.BLL.DTOs.Supplier;
using Deliver.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deliver.BLL.Interfaces
{
    public interface ISupplierServices
    {
        Task<Result> CreateSupplierAsync(SupplierRequest request);

        Task<Result<bool>> UpdateSupplierAsync(int id, SupplierRequest request);

        Task<bool> DeleteSupplierAsync(int id);

        Task<Result<SupplierResponse>> GetSupplierByIdAsync(int id);

    }
}
