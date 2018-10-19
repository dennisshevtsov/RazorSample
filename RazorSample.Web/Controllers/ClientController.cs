using Mapster;
using Microsoft.AspNetCore.Mvc;
using RazorSample.Web.Commands;
using RazorSample.Web.Extensions;
using RazorSample.Web.Queries;
using RazorSample.Web.Services;
using RazorSample.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorSample.Web.Controllers
{
  public sealed class ClientController : Controller
  {
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
      _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
    }

    [HttpGet]
    public async Task<IActionResult> Index(SearchClientQuery query)
    {
      var vm = new ClientListVm().Query(query);

      var commandExecutionResult = await _clientService.HandleAsync(query);

      if (commandExecutionResult.HasError == false)
      {
        var clientListItems = commandExecutionResult.Result.Adapt<IEnumerable<ClientListItemVm>>();

        vm.Items(clientListItems);
      }

      return View("ListView", vm);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CreateClientQuery query)
    {
      var vm = new ClientAddFormVm();

      var commandExecutionResult = await _clientService.HandleAsync(query);

      if (commandExecutionResult.HasError == false)
      {
        var command = commandExecutionResult.Result.Adapt<CreateClientCommand>();

        vm.Command(command);
      }

      return View("AddView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateClientQuery query, CreateClientCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var vm = new ClientAddFormVm().Command(command);

        return View("AddView", vm);
      }

      await _clientService.HandleAsync(command);

      return RedirectToAction(nameof(Edit), new UpdateClientQuery(command.ClientId));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(UpdateClientQuery query)
    {
      var vm = new ClientEditFormVm().Query(query);

      var commandExecutionResult = await _clientService.HandleAsync(query);

      if (commandExecutionResult.HasError == false)
      {
        var command = commandExecutionResult.Result.Adapt<UpdateClientCommand>();

        vm.Command(command);
      }

      return View("EditView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateClientQuery query, UpdateClientCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var vm = new ClientEditFormVm().Query(query)
                                       .Command(command);
      }

      await _clientService.HandleAsync(command);

      return RedirectToAction(nameof(Edit), new UpdateClientQuery(command.ClientId));
    }
  }
}
