using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorSample.Data
{
  public interface IRepository
  {
    Task<TEntity> FirstAsync<TEntity>(Specification<TEntity> specification) where TEntity : class;
    Task<Page<TEntity>> PageAsync<TEntity>(Specification<TEntity> specification, int pageSize, int pageNo) where TEntity : class;

    Task<TEntity> InsertAsync<TEntity>(TEntity entity) where TEntity : class;
    Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
    Task UpdateAsync<TEntity>(TEntity entity, IEnumerable<string> properties) where TEntity : class;

    Task RemoveAsync<TEntity>(TEntity entity) where TEntity : class;

    Task<ITransaction> BeginTransactionAsync();
  }
}
