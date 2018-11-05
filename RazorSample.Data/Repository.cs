using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorSample.Data
{
  public sealed class Repository : IRepository
  {
    private readonly DbContext _dbContext;

    public Repository(DbContext dbContext)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<TEntity> FirstAsync<TEntity>(Specification<TEntity> specification) where TEntity : class
    {
      return await specification.Apply(_dbContext.Set<TEntity>()).FirstOrDefaultAsync();
    }

    public async Task<Page<TEntity>> PageAsync<TEntity>(Specification<TEntity> specification, int pageSize, int pageNo) where TEntity : class
    {
      var query = specification.Apply(_dbContext.Set<TEntity>());

      var total = await query.CountAsync();
      var totalPages = total / pageSize;

      if (total % pageSize > 0)
      {
        ++totalPages;
      }

      var currentPageNo = pageNo;

      if (pageNo < 0 && pageNo >= totalPages)
      {
        currentPageNo = totalPages;
      }

      if (currentPageNo > 0)
      {
        query = query.Skip(pageSize * currentPageNo);
      }

      query = query.Take(pageSize);

      var entities = await query.ToArrayAsync();

      return new Page<TEntity>(entities, currentPageNo, totalPages, pageSize);
    }

    public async Task<TEntity> InsertAsync<TEntity>(TEntity entity) where TEntity : class
    {
      var entry = await _dbContext.AddAsync(entity);

      await _dbContext.SaveChangesAsync();

      return entry.Entity;
    }

    public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
    {
      _dbContext.Update(entity);
      await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync<TEntity>(TEntity entity, IEnumerable<string> properties) where TEntity : class
    {
      var entry = _dbContext.Attach(entity);

      foreach (var property in properties)
      {
        entry.Property(property).IsModified = true;
      }

      await _dbContext.SaveChangesAsync();
    }
  }
}
