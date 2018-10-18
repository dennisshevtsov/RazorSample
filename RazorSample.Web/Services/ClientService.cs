using System;
using System.Threading.Tasks;
using RazorSample.Data;
using RazorSample.Data.Entities;
using RazorSample.Data.Specifications;
using RazorSample.Web.Queries;

namespace RazorSample.Web.Services
{
  public sealed class ClientService : IClientService
  {
    private readonly IRepository _repository;

    public ClientService(IRepository repository)
    {
      _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<QueryExecutionResult<Page<ClientEntity>>> HandleAsync(SearchClientQuery query)
    {
      var clients = await _repository.PageAsync(new ClientLikeSpecification(query.ClientNo), query.PageSize, query.PageNo);
      var queryExecutionResult = new QueryExecutionResult<Page<ClientEntity>>(clients);

      return queryExecutionResult;
    }
  }
}
