
using Deliver.Dal.Data;
using Deliver.Entities.Entities;
using Deliver.Entities.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Deliver.Dal.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Users = new Repository<ApplicationUser>(_context);

    }
    public IRepository<ApplicationUser> Users { get; }

    public IRepository<Customer> Customers => throw new NotImplementedException();

    public IRepository<Delivery> Deliveries => throw new NotImplementedException();

    public IRepository<Supplier> Suppliers { get; }

    public IRepository<ParentCategory> ParentCategories { get; }

    public IRepository<SubCategory> SubCategories { get; }

    public async Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> action)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var result = await action();
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}