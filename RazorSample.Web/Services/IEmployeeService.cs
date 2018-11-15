using RazorSample.Data;
using RazorSample.Data.Entities;
using RazorSample.Web.Commands;
using RazorSample.Web.Queries;

namespace RazorSample.Web.Services
{
  public interface IEmployeeService :
    IQueryHandler<SearchEmployeeQuery, Page<EmployeeEntity>>,
    IQueryHandler<UpdateEmployeeQuery, UpdateEmployeeCommand>,
    IQueryHandler<UpdateEmployeeAddressesQuery, EmployeeEntity>,
    ICommandHandler<UpdateEmployeeCommand>,
    ICommandHandler<CreateEmployeeCommand>
  { }
}
