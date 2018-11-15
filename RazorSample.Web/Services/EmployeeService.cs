using System;
using System.Threading.Tasks;
using RazorSample.Data;
using RazorSample.Data.Entities;
using RazorSample.Data.Specifications;
using RazorSample.Web.Commands;
using RazorSample.Web.Queries;

namespace RazorSample.Web.Services
{
  public sealed class EmployeeService : IEmployeeService
  {
    private readonly IRepository _repository;

    public EmployeeService(IRepository repository)
    {
      _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<QueryExecutionResult<Page<EmployeeEntity>>> HandleAsync(SearchEmployeeQuery query)
    {
      var employeeEntities = await _repository.PageAsync(new EmployeesLikeSpecification(query.EmployeeNo),
                                                         query.PageSize,
                                                         query.PageNo);
      var queryExecutionResult = new QueryExecutionResult<Page<EmployeeEntity>>(employeeEntities);

      return queryExecutionResult;
    }

    public async Task<QueryExecutionResult<UpdateEmployeeCommand>> HandleAsync(UpdateEmployeeQuery query)
    {
      var employeeEntity = await _repository.FirstAsync(new EmployeeWithIdSpecification(query.EmployeeId));
      var updateEmployeeCommand = new UpdateEmployeeCommand(employeeEntity);
      var queryExecutionResult = new QueryExecutionResult<UpdateEmployeeCommand>(updateEmployeeCommand);

      return queryExecutionResult;
    }

    public async Task<QueryExecutionResult<EmployeeEntity>> HandleAsync(UpdateEmployeeAddressesQuery query)
    {
      var employeeEntity = await _repository.FirstAsync(new EmployeeWithIdSpecification(query.EmployeeId));
      var queryExecutionResult = new QueryExecutionResult<EmployeeEntity>(employeeEntity);

      return queryExecutionResult;
    }

    public async Task<CommandExecutionResult> HandleAsync(UpdateEmployeeCommand command)
    {
      var changeEntry = new ChangeEntry<EmployeeEntity>().Key(employee => employee.EmployeeId, command.EmployeeId)
                                                         .Property(employee => employee.FirstName, command.FirstName)
                                                         .Property(employee => employee.LastName, command.LastName)
                                                         .Property(employee => employee.EmployeeNo, command.EmployeeNo);
                                                         //.Property(employee => employee.Email, command.Email)
                                                         //.Property(employee => employee.Phone, command.Phone)
                                                         //.Property(employee => employee.Address, command.Address);

      await _repository.UpdateAsync(changeEntry.Entity, changeEntry.Properties);

      foreach (var email in command.Emails)
      {
        var emailEntity = new EmailEntity();

        emailEntity.Email = email;
        emailEntity.SubjectId = command.EmployeeId;

        await _repository.InsertAsync(emailEntity);
      }

      foreach (var address in command.Addresses)
      {
        var addressEntity = new AddressEntity();

        addressEntity.Address = address;
        addressEntity.SubjectId = command.EmployeeId;

        await _repository.InsertAsync(addressEntity);
      }

      foreach (var phone in command.Phones)
      {
        var phoneEntity = new PhoneEntity();

        phoneEntity.Phone = phone;
        phoneEntity.SubjectId = command.EmployeeId;

        await _repository.InsertAsync(phoneEntity);
      }

      foreach (var im in command.Ims)
      {
        var imEntity = new ImEntity();

        imEntity.Im = im;
        imEntity.SubjectId = command.EmployeeId;

        await _repository.InsertAsync(imEntity);
      }

      return CommandExecutionResult.Success;
    }

    public async Task<CommandExecutionResult> HandleAsync(CreateEmployeeCommand command)
    {
      var employeeEntity = new EmployeeEntity();

      employeeEntity.EmployeeId = Guid.NewGuid();
      employeeEntity.FirstName = command.FirstName;
      employeeEntity.LastName = command.LastName;
      employeeEntity.EmployeeNo = command.EmployeeNo;

      await _repository.InsertAsync(employeeEntity);

      var emailEntity = new EmailEntity();

      emailEntity.Email = command.Email;
      emailEntity.SubjectId = employeeEntity.EmployeeId;

      await _repository.InsertAsync(emailEntity);

      return CommandExecutionResult.Success;
    }
  }
}
