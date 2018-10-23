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
      vm.BreadcrumbActions = new[] { new Link("self", "Employees", Url.Action(nameof(Index), query)) };
      vm.NavActions = new[] { new Link("employee", "Employees", Url.Action(nameof(Index))),
                              new Link("client", "Clients", Url.Action("index", "client")), };
      vm.SelectedNavAction = "employee";

      return View("ListView", vm);
    }

    [HttpGet]
    public IActionResult Add()
    {
      var vm = new EmployeeAddFormVm().Command(_randomGenerator.RandomEmployee());

      return View("AddView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateEmployeeCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var vm = new EmployeeAddFormVm().Command(command);

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

      return View("EditView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(GetEmployeeQuery query, UpdateEmployeeCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var vm = new EmployeeEditFormVm().Query(query)
                                         .Command(command);

        return View("EditView", vm);
      }

      await _employeeService.HandleAsync(command);

      return RedirectToAction(nameof(Index), new SearchEmployeesQuery(command.EmployeeNo));
    }
  }
}
