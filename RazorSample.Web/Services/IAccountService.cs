using RazorSample.Data.Entities;
using RazorSample.Web.Queries;

namespace RazorSample.Web.Services
{
  public interface IAccountService : IQueryHandler<LoginQuery, AccountEntity>
  { }
}
