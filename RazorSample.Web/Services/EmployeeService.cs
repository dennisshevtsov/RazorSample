using System;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
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

    public async Task<QueryExecutionResult<EmployeeEntity>> HandleAsync(AddEmployeeAddressQuery query)
    {
      var employeeEntity = await _repository.FirstAsync(new EmployeeWithIdSpecification(query.EmployeeId));
      var queryExecutionResult = new QueryExecutionResult<EmployeeEntity>(employeeEntity);

      return queryExecutionResult;
    }

    public async Task<QueryExecutionResult<EmployeeEntity>> HandleAsync(RemoveEmployeeAddressQuery query)
    {
      var employeeEntity = await _repository.FirstAsync(new EmployeeWithIdSpecification(query.EmployeeId));
      var queryExecutionResult = new QueryExecutionResult<EmployeeEntity>(employeeEntity);

      return queryExecutionResult;
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

    public async Task<CommandExecutionResult> HandleAsync(UpdateEmployeeGeneralInfoCommand command)
    {
      var changeEntry = new ChangeEntry<EmployeeEntity>().Key(employee => employee.EmployeeId, command.EmployeeId)
                                                         .Property(employee => employee.FirstName, command.FirstName)
                                                         .Property(employee => employee.LastName, command.LastName)
                                                         .Property(employee => employee.EmployeeNo, command.EmployeeNo);

      await _repository.UpdateAsync(changeEntry.Entity, changeEntry.Properties);

      return CommandExecutionResult.Success;
    }

    public async Task<CommandExecutionResult> HandleAsync(UpdateEmployeeAddressesCommand command)
    {
      using (var transaction = await _repository.BeginTransactionAsync())
        try
        {
          await _repository.RemoveAsync(new AddressesOfEmployeeSpecification(command.EmployeeId));

          var newAddresses = command.Addresses.Select(address => new AddressEntity
          {
            SubjectId = command.EmployeeId,
            Address = address,
            City = address,
            Country = address,
            Zip = address,
          });

          foreach (var address in newAddresses)
          {
            await _repository.InsertAsync(address);
          }

          await transaction.CommitAsync();
        }
        catch
        {
          await transaction.RollbackAsync();

          return new CommandExecutionResult("An error occured.");
        }

      return CommandExecutionResult.Success;
    }

    public async Task<CommandExecutionResult> HandleAsync(UpdateEmployeeEmailsCommand command)
    {
      using (var transaction = await _repository.BeginTransactionAsync())
        try
        {
          await _repository.RemoveAsync(new EmailsOfEmployeeSpecification(command.EmployeeId));

          var newEmails = command.Emails.Select(email => new EmailEntity
          {
            SubjectId = command.EmployeeId,
            Email = email,
          });

          foreach (var email in newEmails)
          {
            await _repository.InsertAsync(email);
          }

          await transaction.CommitAsync();
        }
        catch
        {
          await transaction.RollbackAsync();

          return new CommandExecutionResult("An error occured.");
        }

      return CommandExecutionResult.Success;
    }

    public async Task<CommandExecutionResult> HandleAsync(UpdateEmployeePhonesCommand command)
    {
      using (var transaction = await _repository.BeginTransactionAsync())
        try
        {
          await _repository.RemoveAsync(new PhonesOfEmployeeSpecification(command.EmployeeId));

          var newPhones = command.Phones.Select(phone => new PhoneEntity
          {
            SubjectId = command.EmployeeId,
            Phone = phone,
          });

          foreach (var phone in newPhones)
          {
            await _repository.InsertAsync(phone);
          }

          await transaction.CommitAsync();
        }
        catch
        {
          await transaction.RollbackAsync();

          return new CommandExecutionResult("An error occured.");
        }

      return CommandExecutionResult.Success;
    }

    public async Task<CommandExecutionResult> HandleAsync(UpdateEmployeeImsCommand command)
    {
      using (var transaction = await _repository.BeginTransactionAsync())
        try
        {
          await _repository.RemoveAsync(new ImsOfEmployeeSpecification(command.EmployeeId));

          var newIms = command.Ims.Select(im => new ImEntity
          {
            SubjectId = command.EmployeeId,
            Im = im,
          });

          foreach (var im in newIms)
          {
            await _repository.InsertAsync(im);
          }

          await transaction.CommitAsync();
        }
        catch
        {
          await transaction.RollbackAsync();

          return new CommandExecutionResult("An error occured.");
        }

      return CommandExecutionResult.Success;
    }

    public async Task<CommandExecutionResult> HandleAsync(AddEmployeeAddressCommand command)
    {
      //TypeAdapterConfig<AddEmployeeAddressCommand, AddressEntity>.NewConfig()
      //                                                           .Map(entity => entity.SubjectId, cmd => cmd.EmployeeId);

      var addressEntity = command.Adapt<AddressEntity>();

      await _repository.InsertAsync(addressEntity);

      return CommandExecutionResult.Success;
    }

    public async Task<CommandExecutionResult> HandleAsync(RemoveEmployeeAddressCommand command)
    {
      await _repository.RemoveAsync(new AddressForIdSpecification(command.AddressId));

      return CommandExecutionResult.Success;
    }
  }
}
