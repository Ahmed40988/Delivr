using Deliver.Entities.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Deliver.Entities.Interfaces;
public interface IUnitOfWork:IDisposable
{
    public IRepository<ApplicationUser> Users { get; }
  public IRepository<Customer> Customers { get; }
    public IRepository<Delivery>Deliveries { get; }
    public IRepository<Supplier> Suppliers { get; }
    public IRepository<ParentCategory> ParentCategories { get; }
    public IRepository<SubCategory> SubCategories { get; }
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
    Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> action);
}
