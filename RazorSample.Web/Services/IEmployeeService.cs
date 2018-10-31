using RazorSample.Data;
using RazorSample.Web.Commands;
using RazorSample.Web.Queries;
using RazorSample.Web.ViewModels;

namespace RazorSample.Web.Services
{
  public interface IEmployeeService :
    IQueryHandler<SearchEmployeesQuery, Page<EmployeeListItemVm>>,
    IQueryHandler<UpdateEmployeeQuery, UpdateEmployeeCommand>,
    ICommandHandler<UpdateEmployeeCommand>,
    ICommandHandler<CreateEmployeeCommand>
  { }
}
