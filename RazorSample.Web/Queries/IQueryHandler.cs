using System.Threading.Tasks;

namespace RazorSample.Web.Queries
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : class
        where TResult : class
    {
        Task<QueryExecutionResult<TResult>> HandleAsync(TQuery query);
    }
}
