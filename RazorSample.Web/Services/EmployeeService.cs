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

    public async Task<CommandExecutionResult> HandleAsync(UpdateEmployeeCommand command)
    {
      var changeEntry = new ChangeEntry<EmployeeEntity>().Key(employee => employee.EmployeeId, command.EmployeeId)
                                                         .Property(employee => employee.FirstName, command.FirstName)
                                                         .Property(employee => employee.LastName, command.LastName)
                                                         .Property(employee => employee.EmployeeNo, command.EmployeeNo)
                                                         .Property(employee => employee.Email, command.Email);

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
      employeeEntity.Email = command.Email;

      await _repository.InsertAsync(employeeEntity);

      return CommandExecutionResult.Success;
    }
  }
}
