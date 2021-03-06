﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using RazorSample.Data;
using RazorSample.Data.Entities;
using RazorSample.Data.Specifications;
using RazorSample.Random;
using RazorSample.Web.Commands;
using RazorSample.Web.Extensions;
using RazorSample.Web.Queries;

namespace RazorSample.Web.Services
{
  public sealed class ClientService : IClientService
  {
    private readonly IRepository _repository;
    private readonly IRandomGenerator _randomGenerator;

    public ClientService(IRepository repository, IRandomGenerator randomGenerator)
    {
      _repository = repository ?? throw new ArgumentNullException(nameof(repository));
      _randomGenerator = randomGenerator ?? throw new ArgumentNullException(nameof(randomGenerator));
    }

    public async Task<QueryExecutionResult<Page<ClientEntity>>> HandleAsync(SearchClientQuery query)
    {
      var clients = await _repository.PageAsync(new ClientLikeSpecification(query.ClientNo), query.PageSize, query.PageNo);
      var queryExecutionResult = new QueryExecutionResult<Page<ClientEntity>>(clients);

      return queryExecutionResult;
    }

    public async Task<QueryExecutionResult<ClientEntity>> HandleAsync(CreateClientQuery query)
    {
      var clientEntity = _randomGenerator.RandomClient();

      if (query.ClientOwnerId != null)
      {
        clientEntity.ClientOwnerId = query.ClientOwnerId.Value;

        var clientOwner = await _repository.FirstAsync(new EmployeeWithIdSpecification(query.ClientOwnerId.Value));

        if (clientOwner == null)
        {
          return new QueryExecutionResult<ClientEntity>($"Employee with ID {query.ClientOwnerId.Value} is not found.");
        }

        clientEntity.ClientOwner = clientOwner;
      }

      var queryExecutionResult = new QueryExecutionResult<ClientEntity>(clientEntity);

      return queryExecutionResult;
    }

    public async Task<QueryExecutionResult<ClientEntity>> HandleAsync(UpdateClientQuery query)
    {
      var clientEntity = await _repository.FirstAsync(new ClientWithIdSpecification(query.ClientId));
      var queryExecutionResult = new QueryExecutionResult<ClientEntity>(clientEntity);

      return queryExecutionResult;
    }

    public async Task<QueryExecutionResult<IEnumerable<EmployeeEntity>>> HandleAsync(SearchClientOwnerQuery query)
    {
      var employees = await _repository.PageAsync(new EmployeesWithNameLikeSpecification(query.ClientOwnerNamePart), 10, 0);
      var queryExecutionResult = new QueryExecutionResult<IEnumerable<EmployeeEntity>>(employees);

      return queryExecutionResult;
    }

    public async Task<QueryExecutionResult<EmployeeEntity>> HandleAsync(SelectClientOwnerQuery query)
    {
      var employeeEntity = await _repository.FirstAsync(new EmployeeWithIdSpecification(query.ClientOwnerId));
      var queryExecutionResult = new QueryExecutionResult<EmployeeEntity>(employeeEntity);

      return queryExecutionResult;
    }

    public async Task<CommandExecutionResult> HandleAsync(CreateClientCommand command)
    {
      command.ClientId = Guid.NewGuid();

      var clientEntity = command.Adapt<ClientEntity>();

      await _repository.InsertAsync(clientEntity);

      return CommandExecutionResult.Success;
    }

    public async Task<CommandExecutionResult> HandleAsync(UpdateClientCommand command)
    {
      var clientEntity = command.Adapt<ClientEntity>();
      var properties = typeof(UpdateClientCommand).GetProperties()
                                                  .Where(property => property.Name.Equals(
                                                    nameof(ClientEntity.ClientId),
                                                    StringComparison.InvariantCultureIgnoreCase) == false)
                                                  .Select(property => property.Name);

      await _repository.UpdateAsync(clientEntity, properties);

      return CommandExecutionResult.Success;
    }
  }
}
