using RazorSample.Data;
using RazorSample.Data.Entities;
using RazorSample.Web.Commands;
using RazorSample.Web.Queries;

namespace RazorSample.Web.Services
{
  public interface IEmployeeService :
    IQueryHandler<SearchEmployeeQuery, Page<EmployeeEntity>>,
    IQueryHandler<UpdateEmployeeGeneralInfoQuery, EmployeeEntity>,
    IQueryHandler<SearchEmployeeAddressQuery, EmployeeEntity>,
    IQueryHandler<SearchEmployeeEmailQuery, EmployeeEntity>,
    IQueryHandler<SearchEmployeePhoneQuery, EmployeeEntity>,
    IQueryHandler<SearchEmployeeImQuery, EmployeeEntity>,
    IQueryHandler<AddEmployeeAddressQuery, EmployeeEntity>,
    IQueryHandler<RemoveEmployeeAddressQuery, EmployeeEntity>,
    IQueryHandler<AddEmployeeEmailQuery, EmployeeEntity>,
    IQueryHandler<RemoveEmployeeEmailQuery, EmployeeEntity>,
    IQueryHandler<AddEmployeePhoneQuery, EmployeeEntity>,
    IQueryHandler<RemoveEmployeePhoneQuery, EmployeeEntity>,
    IQueryHandler<AddEmployeeImQuery, EmployeeEntity>,
    IQueryHandler<RemoveEmployeeImQuery, EmployeeEntity>,
    ICommandHandler<CreateEmployeeCommand>,
    ICommandHandler<UpdateEmployeeGeneralInfoCommand>,
    ICommandHandler<UpdateEmployeeAddressesCommand>,
    ICommandHandler<UpdateEmployeeEmailsCommand>,
    ICommandHandler<UpdateEmployeePhonesCommand>,
    ICommandHandler<UpdateEmployeeImsCommand>,
    ICommandHandler<AddEmployeeAddressCommand>,
    ICommandHandler<RemoveEmployeeAddressCommand>,
    ICommandHandler<AddEmployeeEmailCommand>,
    ICommandHandler<RemoveEmployeeEmailCommand>,
    ICommandHandler<AddEmployeePhoneCommand>,
    ICommandHandler<RemoveEmployeePhoneCommand>,
    ICommandHandler<AddEmployeeImCommand>,
    ICommandHandler<RemoveEmployeeImCommand>
  { }
}
