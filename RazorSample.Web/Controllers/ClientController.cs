using Mapster;
using Microsoft.AspNetCore.Mvc;
using RazorSample.Data.Entities;
using RazorSample.Hr;
using RazorSample.Vm;
using RazorSample.Web.Commands;
using RazorSample.Web.Extensions;
using RazorSample.Web.Queries;
using RazorSample.Web.Services;
using System;
using System.Threading.Tasks;

namespace RazorSample.Web.Controllers
{
  public sealed class ClientController : Controller
  {
    private readonly IClientService _clientService;
    private readonly IResourceBuilder _builder;

    public ClientController(IClientService clientService, IResourceBuilder builder)
    {
      _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
      _builder = builder ?? throw new ArgumentNullException(nameof(builder));
    }

    [HttpGet]
    public async Task<IActionResult> Index(SearchClientQuery query)
    {
      _builder.Link(Url.AppLink(RelTypes.Breadcrumb, "Clients", nameof(Index), nameof(ClientController)))
              .Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Clients", nameof(ClientController.Index), nameof(ClientController)))
              .Link(Url.AppLink(RelTypes.Action, "+ new client", nameof(ClientController.Add), nameof(ClientController)))
              .Link(Url.AppLink(RelTypes.Self, "Clients", nameof(Index), nameof(ClientController)));

      var commandExecutionResult = await _clientService.HandleAsync(query);

      if (commandExecutionResult.HasError == false)
      {
        if (commandExecutionResult.Result.PageNo > commandExecutionResult.Result.FirstPageNo)
        {
          _builder.Link(Url.AppLink(RelTypes.First, "First", nameof(Index), nameof(ClientController), new SearchClientQuery(query.ClientNo, commandExecutionResult.Result.FirstPageNo)))
                  .Link(Url.AppLink(RelTypes.Prev, "Previous", nameof(Index), nameof(ClientController), new SearchClientQuery(query.ClientNo, commandExecutionResult.Result.PageNo - 1)));
        }

        if (commandExecutionResult.Result.PageNo < commandExecutionResult.Result.LastPageNo)
        {
          _builder.Link(Url.AppLink(RelTypes.Next, "Next", nameof(Index), nameof(ClientController), new SearchClientQuery(query.ClientNo, commandExecutionResult.Result.PageNo + 1)))
                  .Link(Url.AppLink(RelTypes.Last, "Last", nameof(Index), nameof(ClientController), new SearchClientQuery(query.ClientNo, commandExecutionResult.Result.LastPageNo)));
        }

        foreach (var client in commandExecutionResult.Result)
        {
          _builder.Embedded(RelTypes.Row)
                  .Property(nameof(client.Name), "Name", client.Name)
                  .Property(nameof(client.ClientNo), "Client No", client.ClientNo)
                  .Property(nameof(client.Created), "Created", client.Created)
                  .Link(Url.AppLink(RelTypes.Self, "Name", nameof(Edit), nameof(ClientController), new UpdateClientQuery(client.ClientId)));
        }
      }

      var vm = _builder.Build()
                       .ToGridVm();

      return View("GridView", vm);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CreateClientQuery query)
    {
      var commandExecutionResult = await _clientService.HandleAsync(query);
      var vm = BuildAddForm(commandExecutionResult.Result);

      return View("FormView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateClientQuery query, CreateClientCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var vm = BuildAddForm(command.Adapt<ClientEntity>());

        return View("FormView", vm);
      }

      await _clientService.HandleAsync(command);

      return Redirect(Url.AppUri(nameof(Edit), nameof(ClientController), new UpdateClientQuery(command.ClientId)));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(UpdateClientQuery query)
    {
      var commandExecutionResult = await _clientService.HandleAsync(query);
      var vm = BuildEditForm(commandExecutionResult.Result);

      return View("FormView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateClientQuery query, UpdateClientCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var vm = BuildEditForm(command.Adapt<ClientEntity>());

        return View("FormView", vm);
      }

      await _clientService.HandleAsync(command);

      return Redirect(Url.AppUri(nameof(Edit), nameof(ClientController), new UpdateClientQuery(command.ClientId)));
    }

    [HttpPost("/client-owner/search")]
    public async Task<IActionResult> ClientOwnerSearch(SearchClientOwnerQuery query)
    {
      var builder = _builder.Property(nameof(ClientCommandBase.ClientOwnerId), "Client Owner", null)
                            .Link(Url.AppLink(RelTypes.Action, "Clear", nameof(ClientOwnerClear), nameof(ClientController)))
                            .Embedded(RelTypes.Search)
                            .Property(nameof(SearchClientOwnerQuery.ClientOwnerNamePart), "", query.ClientOwnerNamePart)
                            .Link(Url.AppLink(RelTypes.Self, "Search", nameof(ClientOwnerSearch), nameof(ClientController)));

      var employees = await _clientService.HandleAsync(query);

      foreach (var employee in employees.Result)
      {
        builder.Link(Url.AppLink(RelTypes.Action, employee.FullName, nameof(ClientOwnerSelect), nameof(ClientController), new SelectClientOwnerQuery(employee.EmployeeId)));
      }

      var vm = new SelectVm(_builder.Build());

      return PartialView("SelectPartialView", vm);
    }

    [HttpPost("/client-owner/clear")]
    public IActionResult ClientOwnerClear()
    {
      BuildClientOwnerSelect(_builder, null);

      var vm = new SelectVm(_builder.Build());

      return PartialView("SelectPartialView", vm);
    }

    [HttpPost("/client-owner/select")]
    public async Task<IActionResult> ClientOwnerSelect(SelectClientOwnerQuery query)
    {
      var queryExecutionResult = await _clientService.HandleAsync(query);

      if (queryExecutionResult.HasError)
      {
        BuildClientOwnerSelect(_builder, null);
      }
      else
      {
        BuildClientOwnerSelect(_builder, queryExecutionResult.Result);
      }

      var vm = new SelectVm(_builder.Build());

      return PartialView("SelectPartialView", vm);
    }

    private IResourceBuilder BuildFormBase(ClientEntity clientEntity)
    {
      _builder.Link(Url.AppLink(RelTypes.Breadcrumb, "Clients", nameof(Index), nameof(ClientController)))
              .Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Clients", nameof(ClientController.Index), nameof(ClientController)))
              .Property(nameof(ClientCommandBase.Name), "Name", clientEntity.Name)
              .Property(nameof(ClientCommandBase.ClientNo), "Client No", clientEntity.ClientNo)
              .Property(nameof(ClientCommandBase.OrganizationNo), "Organization No", clientEntity.OrganizationNo);

      BuildClientOwnerSelect(_builder.Embedded(RelTypes.Select), clientEntity.ClientOwner);

      return _builder;
    }

    private IFormVm BuildAddForm(ClientEntity clientEntity) =>
      BuildFormBase(clientEntity).Link(Url.AppLink(RelTypes.Breadcrumb, "New Client", nameof(Add), nameof(ClientController)))
                                 .Link(Url.AppLink(RelTypes.Self, "New Client", nameof(Add), nameof(ClientController)))
                                 .Build()
                                 .ToFormVm();

    private IFormVm BuildEditForm(ClientEntity clientEntity)
    {
      BuildFormBase(clientEntity).Link(Url.AppLink(RelTypes.Breadcrumb, clientEntity.Name, nameof(Edit), nameof(ClientController), new UpdateClientQuery(clientEntity.ClientId)))
                                 .Link(Url.AppLink(RelTypes.Self, clientEntity.Name, nameof(Edit), nameof(ClientController), new UpdateClientQuery(clientEntity.ClientId)))
                                 .Link(Url.AppLink(RelTypes.Action, "+ new client", nameof(ClientController.Add), nameof(ClientController)));

      _builder.Link(Url.AppLink(RelTypes.Tab, "General Info", nameof(Edit), nameof(ClientController), new UpdateClientQuery(clientEntity.ClientId)))
              .Link(Url.AppLink(RelTypes.Tab, "Addresses", nameof(Index), nameof(ClientController)))
              .Link(Url.AppLink(RelTypes.Tab, "Emails", nameof(Index), nameof(ClientController)))
              .Link(Url.AppLink(RelTypes.Tab, "Phones", nameof(Index), nameof(ClientController)))
              .Link(Url.AppLink(RelTypes.Tab, "IM", nameof(Index), nameof(ClientController)));

      return _builder.Build()
                     .ToFormVm();
    }

    private IResourceBuilder BuildClientOwnerSelect(IResourceBuilder builder, EmployeeEntity employeeEntity)
    {
      if (employeeEntity != null)
      {
        builder.Property(nameof(ClientCommandBase.ClientOwnerId), "Client Owner", employeeEntity.EmployeeId, employeeEntity.FullName)
               .Link(Url.AppLink(RelTypes.Action, "Clear", nameof(ClientOwnerClear), nameof(ClientController)));
      }
      else
      {
        builder.Property(nameof(ClientCommandBase.ClientOwnerId), "Client Owner", null);
      }

      builder.Embedded(RelTypes.Search)
             .Property(nameof(SearchClientOwnerQuery.ClientOwnerNamePart), "", "")
             .Link(Url.AppLink(RelTypes.Self, "Search", nameof(ClientOwnerSearch), nameof(ClientController)));

      return _builder;
    }
  }
}
