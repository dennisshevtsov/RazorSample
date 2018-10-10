using System.Linq;

namespace RazorSample.Data
{
    public abstract class Specification<TEntity> where TEntity : class
    {
        internal protected abstract IQueryable<TEntity> Apply(IQueryable<TEntity> query);
    }
}
