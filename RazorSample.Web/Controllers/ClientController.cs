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

      foreach (var client in commandExecutionResult.Result)
      {
        _builder.Embedded(RelTypes.Row)
                        .Property(new Property(nameof(client.Name), "Name", client.Name))
                        .Property(new Property(nameof(client.ClientNo), "Client No", client.ClientNo))
                        .Property(new Property(nameof(client.Created), "Created", client.Created))
                        .Link(Url.AppLink(RelTypes.Self, "Name", nameof(Edit), nameof(ClientController), new UpdateClientQuery(client.ClientId)));
      }

      var vm = _builder.Build()
                               .ToListVm();

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

      return RedirectToAction(nameof(Edit), new UpdateClientQuery(command.ClientId));
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

      return RedirectToAction(nameof(Edit), new UpdateClientQuery(command.ClientId));
    }

    private IResourceBuilder BuildFormBase(ClientEntity clientEntity) =>
      _builder.Link(Url.AppLink(RelTypes.Breadcrumb, "Clients", nameof(Index), nameof(ClientController)))
              .Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Clients", nameof(ClientController.Index), nameof(ClientController)))
              .Property(new Property(nameof(clientEntity.Name), "Name", clientEntity.Name))
              .Property(new Property(nameof(clientEntity.ClientNo), "Client No", clientEntity.ClientNo))
              .Property(new Property(nameof(clientEntity.OrganizationNo), "Organization No", clientEntity.OrganizationNo));

    private IFormVm BuildAddForm(ClientEntity clientEntity) =>
      BuildFormBase(clientEntity).Link(Url.AppLink(RelTypes.Breadcrumb, "New Client", nameof(Add), nameof(ClientController)))
                                 .Link(Url.AppLink(RelTypes.Self, "New Client", nameof(Add), nameof(ClientController)))
                                 .Build()
                                 .ToFormVm();

    private IFormVm BuildEditForm(ClientEntity clientEntity) =>
      BuildFormBase(clientEntity).Link(Url.AppLink(RelTypes.Breadcrumb, clientEntity.Name, nameof(Edit), nameof(ClientController), new UpdateClientQuery(clientEntity.ClientId)))
                                 .Link(Url.AppLink(RelTypes.Self, clientEntity.Name, nameof(Edit), nameof(ClientController), new UpdateClientQuery(clientEntity.ClientId)))
                                 .Link(Url.AppLink(RelTypes.Action, "+ new client", nameof(ClientController.Add), nameof(ClientController)))
                                 .Build()
                                 .ToFormVm();
  }
}
