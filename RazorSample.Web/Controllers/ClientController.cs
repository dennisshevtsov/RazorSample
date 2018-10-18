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
      var vm = new ClientListVm().Query(query)
                                 .Items((await _clientService.HandleAsync(query)).Result.Adapt<IEnumerable<ClientListItemVm>>());

      return View("ListView", vm);
    }

    [HttpGet]
    public IActionResult Add(CreateClientQuery query)
    {
      var vm = new ClientAddFormVm();

      return View("AddView", vm);
    }

    [HttpPost]
    public IActionResult Add(CreateClientQuery query, CreateClientCommand command)
    {
      return RedirectToAction(nameof(Edit), new UpdateClientQuery(command.ClientId));
    }

    [HttpGet]
    public IActionResult Edit(UpdateClientQuery query)
    {
      var vm = new ClientEditFormVm();

      return View("AddView", vm);
    }

    [HttpPost]
    public IActionResult Edit(UpdateClientQuery query, UpdateClientCommand command)
    {
      return RedirectToAction(nameof(Edit), new UpdateClientQuery(command.ClientId));
    }
  }
}
