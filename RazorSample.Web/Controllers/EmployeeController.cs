using Microsoft.AspNetCore.Mvc;
using RazorSample.Web.Commands;
using RazorSample.Web.Extensions;
using RazorSample.Web.Queries;
using RazorSample.Web.Services;
using RazorSample.Web.ViewModels;
using System;
using System.Threading.Tasks;

namespace RazorSample.Web.Controllers
{
  public sealed class EmployeeController : Controller
  {
    internal const string EmployeeSearchRel = "rs:employee";
    internal const string EmployeeCreateRel = "rs:employee:add";
    internal const string EmployeeUpdateRel = "rs:employee:edit";

    private readonly IEmployeeService _employeeService;
    private readonly IRandomGenerator _randomGenerator;

    public EmployeeController(IEmployeeService employeeService, IRandomGenerator randomGenerator)
    {
      _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
      _randomGenerator = randomGenerator ?? throw new ArgumentNullException(nameof(randomGenerator));
    }

    [HttpGet]
    public async Task<IActionResult> Index(SearchEmployeesQuery query)
    {
      var vm = new EmployeeListVm().Controller(this)
                                   .Query(query)
                                   .Items(await _employeeService.HandleAsync(query));

      vm.Title = "Employee Search";
      vm.Breadcrumbs = new[]
      {
        Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController), query),
      };
      vm.Navs = new[]
      {
        Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)),
        Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)),
      };
      vm.SelectedNav = EmployeeController.EmployeeSearchRel;
      vm.Actions = new[]
      {
        Url.AppLink(EmployeeController.EmployeeCreateRel, "+ new employee", nameof(EmployeeController.Add), nameof(EmployeeController)),
      };

      return View("ListView", vm);
    }

    [HttpGet]
    public IActionResult Add()
    {
      var vm = new EmployeeAddFormVm().Command(_randomGenerator.RandomEmployee());

      vm.Title = "New Employee";
      vm.Breadcrumbs = new[]
      {
        Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)),
        Url.AppLink(EmployeeController.EmployeeCreateRel, "New Employee", nameof(EmployeeController.Add), nameof(EmployeeController)),
      };
      vm.Navs = new[]
      {
        Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)),
        Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)),
      };
      vm.SelectedNav = EmployeeController.EmployeeSearchRel;

      return View("AddView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateEmployeeCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var vm = new EmployeeAddFormVm().Command(command);

        vm.Title = "New Employee";
        vm.Breadcrumbs = new[]
        {
          Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)),
          Url.AppLink(EmployeeController.EmployeeCreateRel, "New Employee", nameof(EmployeeController.Add), nameof(EmployeeController)),
        };
        vm.Navs = new[]
        {
          Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)),
          Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)),
        };
        vm.SelectedNav = EmployeeController.EmployeeSearchRel;

        return View("EditView", vm);
      }

      await _employeeService.HandleAsync(command);

      return RedirectToAction(nameof(Index), new SearchEmployeesQuery(command.EmployeeNo));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(GetEmployeeQuery query)
    {
      var vm = new EmployeeEditFormVm().Query(query)
                                       .Command(await _employeeService.HandleAsync(query));

      vm.Title = $"Employee {vm.Command.LastName}, {vm.Command.FirstName}";
      vm.Breadcrumbs = new[]
      {
        Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)),
        Url.AppLink(EmployeeController.EmployeeUpdateRel, $"{vm.Command.LastName}, {vm.Command.FirstName}", nameof(EmployeeController.Add), nameof(EmployeeController)),
      };
      vm.Navs = new[]
      {
        Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)),
        Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)),
      };
      vm.SelectedNav = EmployeeController.EmployeeSearchRel;
      vm.Actions = new[]
      {
        Url.AppLink(EmployeeController.EmployeeCreateRel, "+ new employee", nameof(EmployeeController.Add), nameof(EmployeeController)),
      };

      return View("EditView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(GetEmployeeQuery query, UpdateEmployeeCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var vm = new EmployeeEditFormVm().Query(query)
                                         .Command(command);

        vm.Title = $"Employee {vm.Command.LastName}, {vm.Command.FirstName}";
        vm.Breadcrumbs = new[]
        {
          Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)),
          Url.AppLink(EmployeeController.EmployeeUpdateRel, $"{vm.Command.LastName}, {vm.Command.FirstName}", nameof(EmployeeController.Add), nameof(EmployeeController)),
        };
        vm.Navs = new[]
        {
          Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)),
          Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)),
        };
        vm.SelectedNav = EmployeeController.EmployeeSearchRel;
        vm.Actions = new[]
        {
          Url.AppLink(EmployeeController.EmployeeCreateRel, "+ new employee", nameof(EmployeeController.Add), nameof(EmployeeController)),
        };

        return View("EditView", vm);
      }

      await _employeeService.HandleAsync(command);

      return RedirectToAction(nameof(Index), new SearchEmployeesQuery(command.EmployeeNo));
    }
  }
}
