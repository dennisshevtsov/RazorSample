using System;
using System.Linq;
using System.Threading.Tasks;
using RazorSample.Data;
using RazorSample.Data.Specifications;
using RazorSample.Web.Queries;
using RazorSample.Web.ViewModels;

namespace RazorSample.Web.Services
{
  public sealed class EmployeeService : IEmployeeService
  {
    private readonly IRepository _repository;

    public EmployeeService(IRepository repository)
    {
      _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<QueryExecutionResult<Page<EmployeeListItemVm>>> HandleAsync(SearchEmployeesQuery query)
    {
      var employeeEntities = await _repository.PageAsync(new EmployeesLikeSpecification(query.EmployeeNo),
                                                         query.PageSize,
                                                         query.PageNo);
      var employeeListItems = employeeEntities.Select(employeeEntity => new EmployeeListItemVm(employeeEntity));
      var employeeListPage = new Page<EmployeeListItemVm>(employeeListItems,
                                                          employeeEntities.PageNo,
                                                          employeeEntities.PageCount,
                                                          employeeEntities.PageSize);
      var queryExecutionResult = new QueryExecutionResult<Page<EmployeeListItemVm>>(employeeListPage);

      return queryExecutionResult;
    }
  }
}
