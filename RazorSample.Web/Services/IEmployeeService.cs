using RazorSample.Data;
using RazorSample.Data.Entities;
using RazorSample.Web.Commands;
using RazorSample.Web.Queries;

namespace RazorSample.Web.Services
{
  public interface IEmployeeService :
    IQueryHandler<SearchEmployeeQuery, Page<EmployeeEntity>>,
    IQueryHandler<UpdateEmployeeGeneralInfoQuery, EmployeeEntity>,
    IQueryHandler<UpdateEmployeeAddressesQuery, EmployeeEntity>,
    IQueryHandler<UpdateEmployeeEmailsQuery, EmployeeEntity>,
    IQueryHandler<UpdateEmployeePhonesQuery, EmployeeEntity>,
    IQueryHandler<UpdateEmployeeImsQuery, EmployeeEntity>,
    IQueryHandler<AddEmployeeAddressQuery, EmployeeEntity>,
    ICommandHandler<CreateEmployeeCommand>,
    ICommandHandler<UpdateEmployeeGeneralInfoCommand>,
    ICommandHandler<UpdateEmployeeAddressesCommand>,
    ICommandHandler<UpdateEmployeeEmailsCommand>,
    ICommandHandler<UpdateEmployeePhonesCommand>,
    ICommandHandler<UpdateEmployeeImsCommand>,
    ICommandHandler<AddEmployeeAddressCommand>
  { }
}
