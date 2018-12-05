using System;
using System.Threading.Tasks;
using RazorSample.Data;
using RazorSample.Data.Entities;
using RazorSample.Data.Specifications;
using RazorSample.Web.Queries;

namespace RazorSample.Web.Services
{
  public sealed class AccountService : IAccountService
  {
    private readonly IRepository _repository;

    public AccountService(IRepository repository)
    {
      _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<QueryExecutionResult<AccountEntity>> HandleAsync(LoginQuery query)
    {
      var accountEntity = await _repository.FirstAsync(new AccountWithEmailSpecification(query.Email));
      var queryExecutionResult = new QueryExecutionResult<AccountEntity>(accountEntity);

      return queryExecutionResult;
    }
  }
}
