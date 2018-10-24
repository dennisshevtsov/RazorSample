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
      var builder = new VmBuilder<SearchEmployeesQuery, EmployeeListItemVm>();
      var vm = builder.Title("Employee Search")
                      .Nav(Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)))
                      .Nav(Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)))
                      .Breadcrumb(Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController), query))
                      .Action(Url.AppLink(EmployeeController.EmployeeCreateRel, "+ new employee", nameof(EmployeeController.Add), nameof(EmployeeController)))
                      .Build()
                      .Controller(this)
                      .Query(query)
                      .Items(await _employeeService.HandleAsync(query));

      vm.SelectedNav = EmployeeController.EmployeeSearchRel;

      return View("ListView", vm);
    }

    [HttpGet]
    public IActionResult Add()
    {
      var builder = new FormVmBuilder<CreateEmployeeCommand>();
      var vm = builder.Title("New Employee")
                      .Breadcrumb(Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)))
                      .Breadcrumb(Url.AppLink(EmployeeController.EmployeeCreateRel, "New Employee", nameof(EmployeeController.Add), nameof(EmployeeController)))
                      .Nav(Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)))
                      .Nav(Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)))
                      .Build()
                      .Command(_randomGenerator.RandomEmployee());

      vm.SelectedNav = EmployeeController.EmployeeSearchRel;

      return View("AddView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateEmployeeCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var builder = new FormVmBuilder<CreateEmployeeCommand>();
        var vm = builder.Title("New Employee")
                        .Breadcrumb(Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)))
                        .Breadcrumb(Url.AppLink(EmployeeController.EmployeeCreateRel, "New Employee", nameof(EmployeeController.Add), nameof(EmployeeController)))
                        .Nav(Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)))
                        .Nav(Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)))
                        .Action(Url.AppLink(EmployeeController.EmployeeCreateRel, "+ new employee", nameof(EmployeeController.Add), nameof(EmployeeController)))
                        .Build()
                        .Command(command);

        vm.SelectedNav = EmployeeController.EmployeeSearchRel;

        return View("EditView", vm);
      }

      await _employeeService.HandleAsync(command);

      return RedirectToAction(nameof(Index), new SearchEmployeesQuery(command.EmployeeNo));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(GetEmployeeQuery query)
    {
      var builder = new FormVmBuilder<GetEmployeeQuery, UpdateEmployeeCommand>();
      var vm = builder.Title("Employee")
                      .Breadcrumb(Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)))
                      .Breadcrumb(Url.AppLink(EmployeeController.EmployeeUpdateRel, "LastName, FirstName", nameof(EmployeeController.Add), nameof(EmployeeController)))
                      .Nav(Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)))
                      .Nav(Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)))
                      .Action(Url.AppLink(EmployeeController.EmployeeCreateRel, "+ new employee", nameof(EmployeeController.Add), nameof(EmployeeController)))
                      .Build()
                      .Query(query)
                      .Command(await _employeeService.HandleAsync(query));

      return View("EditView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(GetEmployeeQuery query, UpdateEmployeeCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var builder = new FormVmBuilder<GetEmployeeQuery, UpdateEmployeeCommand>();
        var vm = builder.Title("Employee")
                        .Breadcrumb(Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)))
                        .Breadcrumb(Url.AppLink(EmployeeController.EmployeeUpdateRel, "LastName, FirstName", nameof(EmployeeController.Add), nameof(EmployeeController)))
                        .Nav(Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)))
                        .Nav(Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)))
                        .Action(Url.AppLink(EmployeeController.EmployeeCreateRel, "+ new employee", nameof(EmployeeController.Add), nameof(EmployeeController)))
                        .Build()
                        .Query(query)
                        .Command(command);

        return View("EditView", vm);
      }

      await _employeeService.HandleAsync(command);

      return RedirectToAction(nameof(Index), new SearchEmployeesQuery(command.EmployeeNo));
    }
  }
}
