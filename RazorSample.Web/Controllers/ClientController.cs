//using Mapster;
//using Microsoft.AspNetCore.Mvc;
//using RazorSample.Vm;
//using RazorSample.Web.Commands;
//using RazorSample.Web.Extensions;
//using RazorSample.Web.Queries;
//using RazorSample.Web.Services;
//using RazorSample.Web.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace RazorSample.Web.Controllers
//{
//  public sealed class ClientController : Controller
//  {
//    internal const string ClientSearchRel = "rs:client";
//    internal const string ClientCreateRel = "rs:client:add";
//    internal const string ClientUpdateRel = "rs:client:edit";

//    private readonly IClientService _clientService;

//    public ClientController(IClientService clientService)
//    {
//      _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
//    }

//    [HttpGet]
//    public async Task<IActionResult> Index(SearchClientQuery query)
//    {
//      //var vm = new ResourceBuilder().Link(Url.AppLink(RelTypes.Breadcrumb, "Clients", nameof(Index), nameof(ClientController)))
//      //                              .Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)))
//      //                              .Link(Url.AppLink(RelTypes.Nav, "Clients", nameof(ClientController.Index), nameof(ClientController)))
//      //                              .Link(Url.AppLink(RelTypes.Action, "+ new client", nameof(ClientController.Add), nameof(ClientController)))
//      //                              .ToListVm();


//      var vm = new ClientListVm().Query(query);

//      var commandExecutionResult = await _clientService.HandleAsync(query);

//      if (commandExecutionResult.HasError == false)
//      {
//        var clientListItems = commandExecutionResult.Result.Adapt<IEnumerable<ClientListItemVm>>();

//        vm.Items(clientListItems);
//      }

//      vm.Title = "Clients";
//      vm.Breadcrumbs = new[]
//      {
//        Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)),
//      };
//      vm.Navs = new[]
//      {
//        Url.AppLink(/*EmployeeController.EmployeeSearchRel*/"", "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)),
//        Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)),
//      };
//      vm.SelectedNav = ClientController.ClientSearchRel;
//      vm.Actions = new[]
//      {
//        Url.AppLink(ClientController.ClientCreateRel, "+ new client", nameof(ClientController.Add), nameof(ClientController)),
//      };

//      return View("ListView", vm);
//    }

//    [HttpGet]
//    public async Task<IActionResult> Add(CreateClientQuery query)
//    {
//      var vm = new ClientAddFormVm();

//      var commandExecutionResult = await _clientService.HandleAsync(query);

//      if (commandExecutionResult.HasError == false)
//      {
//        var command = commandExecutionResult.Result.Adapt<CreateClientCommand>();

//        vm.Command(command);
//      }

//      vm.Title = "New Client";
//      vm.Breadcrumbs = new[]
//      {
//        Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)),
//        Url.AppLink(ClientController.ClientCreateRel, "New client", nameof(ClientController.Add), nameof(ClientController)),
//      };
//      vm.Navs = new[]
//      {
//        Url.AppLink(/*EmployeeController.EmployeeSearchRel*/"", "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)),
//        Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)),
//      };
//      vm.SelectedNav = ClientController.ClientSearchRel;

//      return View("AddView", vm);
//    }

//    [HttpPost]
//    public async Task<IActionResult> Add(CreateClientQuery query, CreateClientCommand command)
//    {
//      if (ModelState.IsValid == false)
//      {
//        var vm = new ClientAddFormVm().Command(command);

//        vm.Title = "New Client";
//        vm.Breadcrumbs = new[]
//        {
//          Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)),
//          Url.AppLink(ClientController.ClientCreateRel, "+ new client", nameof(ClientController.Add), nameof(ClientController)),
//        };
//        vm.Navs = new[]
//        {
//          Url.AppLink(/*EmployeeController.EmployeeSearchRel*/"", "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)),
//          Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)),
//        };
//        vm.SelectedNav = ClientController.ClientSearchRel;
//        vm.SelectedNav = "client";

//        return View("AddView", vm);
//      }

//      await _clientService.HandleAsync(command);

//      return RedirectToAction(nameof(Edit), new UpdateClientQuery(command.ClientId));
//    }

//    [HttpGet]
//    public async Task<IActionResult> Edit(UpdateClientQuery query)
//    {
//      var vm = new ClientEditFormVm().Query(query);

//      var commandExecutionResult = await _clientService.HandleAsync(query);

//      if (commandExecutionResult.HasError == false)
//      {
//        var command = commandExecutionResult.Result.Adapt<UpdateClientCommand>();

//        vm.Command(command);
//      }

//      vm.Title = $"Clients - {vm.Command.Name}";
//      vm.Breadcrumbs = new[]
//      {
//        Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)),
//        Url.AppLink(ClientController.ClientCreateRel, vm.Command.Name, nameof(ClientController.Edit), nameof(ClientController), query),
//      };
//      vm.Navs = new[]
//      {
//        Url.AppLink(/*EmployeeController.EmployeeSearchRel*/"", "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)),
//        Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)),
//      };
//      vm.SelectedNav = ClientController.ClientSearchRel;

//      return View("EditView", vm);
//    }

//    [HttpPost]
//    public async Task<IActionResult> Edit(UpdateClientQuery query, UpdateClientCommand command)
//    {
//      if (ModelState.IsValid == false)
//      {
//        var vm = new ClientEditFormVm().Query(query)
//                                       .Command(command);

//        vm.Title = $"Clients - {vm.Command.Name}";
//        vm.Breadcrumbs = new[]
//        {
//          Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)),
//          Url.AppLink(ClientController.ClientCreateRel, vm.Command.Name, nameof(ClientController.Edit), nameof(ClientController), query),
//        };
//        vm.Navs = new[]
//        {
//          Url.AppLink(/*EmployeeController.EmployeeSearchRel*/"", "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)),
//          Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)),
//        };
//        vm.SelectedNav = ClientController.ClientSearchRel;

//        return View("EditView", vm);
//      }

//      await _clientService.HandleAsync(command);

//      return RedirectToAction(nameof(Edit), new UpdateClientQuery(command.ClientId));
//    }
//  }
//}
