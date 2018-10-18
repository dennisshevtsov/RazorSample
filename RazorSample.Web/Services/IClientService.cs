using RazorSample.Data;
using RazorSample.Data.Entities;
using RazorSample.Web.Queries;

namespace RazorSample.Web.Services
{
  public interface IClientService : IQueryHandler<SearchClientQuery, Page<ClientEntity>>
  { }
}
