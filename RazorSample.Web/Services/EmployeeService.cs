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

    public async Task<QueryExecutionResult<EmployeeEntity>> HandleAsync(UpdateEmployeeGeneralInfoQuery query)
    {
      var employeeEntity = await _repository.FirstAsync(new EmployeeWithIdSpecification(query.EmployeeId));
      var queryExecutionResult = new QueryExecutionResult<EmployeeEntity>(employeeEntity);

      return queryExecutionResult;
    }

    public async Task<QueryExecutionResult<EmployeeEntity>> HandleAsync(UpdateEmployeeAddressesQuery query)
    {
      var employeeEntity = await _repository.FirstAsync(new EmployeeWithIdSpecification(query.EmployeeId));
      var queryExecutionResult = new QueryExecutionResult<EmployeeEntity>(employeeEntity);

      return queryExecutionResult;
    }

    public async Task<QueryExecutionResult<EmployeeEntity>> HandleAsync(UpdateEmployeeEmailsQuery query)
    {
      var employeeEntity = await _repository.FirstAsync(new EmployeeWithIdSpecification(query.EmployeeId));
      var queryExecutionResult = new QueryExecutionResult<EmployeeEntity>(employeeEntity);

      return queryExecutionResult;
    }

    public async Task<QueryExecutionResult<EmployeeEntity>> HandleAsync(UpdateEmployeePhonesQuery query)
    {
      var employeeEntity = await _repository.FirstAsync(new EmployeeWithIdSpecification(query.EmployeeId));
      var queryExecutionResult = new QueryExecutionResult<EmployeeEntity>(employeeEntity);

      return queryExecutionResult;
    }

    public async Task<QueryExecutionResult<EmployeeEntity>> HandleAsync(UpdateEmployeeImsQuery query)
    {
      var employeeEntity = await _repository.FirstAsync(new EmployeeWithIdSpecification(query.EmployeeId));
      var queryExecutionResult = new QueryExecutionResult<EmployeeEntity>(employeeEntity);

      return queryExecutionResult;
    }

    public async Task<CommandExecutionResult> HandleAsync(UpdateEmployeeGeneralInfoCommand command)
    {
      var changeEntry = new ChangeEntry<EmployeeEntity>().Key(employee => employee.EmployeeId, command.EmployeeId)
                                                         .Property(employee => employee.FirstName, command.FirstName)
                                                         .Property(employee => employee.LastName, command.LastName)
                                                         .Property(employee => employee.EmployeeNo, command.EmployeeNo);

      await _repository.UpdateAsync(changeEntry.Entity, changeEntry.Properties);

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
