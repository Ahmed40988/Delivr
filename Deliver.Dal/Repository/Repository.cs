using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Deliver.Dal.Data;
using Deliver.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Deliver.Dal.Repository;
public class Repository<T> : IRepository<T> where T : class
{
    protected ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public T GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public List<string> GetDistinct(Expression<Func<T, string>> col)
    {
        var distinctValues = _context.Set<T>()
                                       .Select(col)
                                       .Distinct()
                                       .ToList();

        return distinctValues;
    }

    public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return query.SingleOrDefault(criteria);
    }

    public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.SingleOrDefaultAsync(criteria);
    }

    public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return query.Where(criteria).ToList();
    }

    public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int skip, int take)
    {
        return _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToList();
    }

    public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? skip, int? take, Expression<Func<T, object>> orderBy = null, bool IsDesc = false)
    {
        IQueryable<T> query = _context.Set<T>().Where(criteria);

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (take.HasValue)
            query = query.Take(take.Value);

        if (orderBy != null)
        {
            if (!IsDesc)
                query = query.OrderBy(orderBy);
            else
                query = query.OrderByDescending(orderBy);
        }

        return query.ToList();
    }

    /// <summary>
    /// Used with jQuery DataTable
    /// </summary>
    /// <param name="criteria">Conditions</param>
    /// <param name="sortColumn">Sort columns of DataTable</param>
    /// <param name="sortColumnDirection">Asc or Desc</param>
    /// <param name="skip">For paging</param>
    /// <param name="take">For paging</param>
    /// <returns>The records based on the current page</returns>
    public IEnumerable<T> FindWithFilters(Expression<Func<T, bool>> criteria = null, string sortColumn = null, string sortColumnDirection = null, int? skip = null, int? take = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (criteria != null)
            query = query.Where(criteria);

        if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
        {
            // Validate sortColumn exists
            if (!typeof(T).GetProperties().Any(p => p.Name == sortColumn))
                throw new ArgumentException($"Invalid sort column: {sortColumn}");

            // Create parameter expression
            var parameter = Expression.Parameter(typeof(T), "x");

            // Create property expression
            var property = Expression.Property(parameter, sortColumn);

            // Create lambda expression
            var lambda = Expression.Lambda(property, parameter);

            // Create order by method call
            string methodName = sortColumnDirection.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";
            var orderByExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { typeof(T), property.Type },
                query.Expression,
                Expression.Quote(lambda)
            );

            query = query.Provider.CreateQuery<T>(orderByExpression);
        }

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (take.HasValue)
            query = query.Take(take.Value);

        return query.ToList();
    }

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.Where(criteria).ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int take, int skip)
    {
        return await _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip, Expression<Func<T, object>> orderBy = null, bool IsDesc = false)
    {
        IQueryable<T> query = _context.Set<T>().Where(criteria);

        if (take.HasValue)
            query = query.Take(take.Value);

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (orderBy != null)
        {
            if (!IsDesc)
                query = query.OrderBy(orderBy);
            else
                query = query.OrderByDescending(orderBy);
        }

        return await query.ToListAsync();
    }

    public T Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public IEnumerable<T> AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
        return entities;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
        return entities;
    }

    public T Update(T entity)
    {
        _context.Update(entity);
        return entity;
    }

    public bool UpdateRange(IEnumerable<T> entities)
    {
        _context.UpdateRange(entities);
        return true;
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public int Count()
    {
        return _context.Set<T>().Count();
    }

    public int Count(Expression<Func<T, bool>> criteria)
    {
        return _context.Set<T>().Count(criteria);
    }

    public async Task<int> CountAsync()
    {
        return await _context.Set<T>().CountAsync();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
    {
        return await _context.Set<T>().CountAsync(criteria);
    }

    public async Task<Int64> MaxAsync(Expression<Func<T, object>> column)
    {
        return Convert.ToInt64(await _context.Set<T>().MaxAsync(column));
    }

    public async Task<Int64> MaxAsync(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> column)
    {
        return Convert.ToInt64(await _context.Set<T>().Where(criteria).MaxAsync(column));
    }

    public Int64 Max(Expression<Func<T, object>> column)
    {
        return Convert.ToInt64(_context.Set<T>().Max(column));
    }

    public Int64 Max(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> column)
    {
        return Convert.ToInt64(_context.Set<T>().Where(criteria).Max(column));
    }

    public bool IsExist(Expression<Func<T, bool>> criteria)
    {
        return _context.Set<T>().Any(criteria);
    }

    public T Last(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy)
    {
        return _context.Set<T>().OrderByDescending(orderBy).FirstOrDefault(criteria);
    }
}