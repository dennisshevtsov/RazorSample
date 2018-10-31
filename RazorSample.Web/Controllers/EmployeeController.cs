using Microsoft.AspNetCore.Mvc;
using RazorSample.Vm;
using RazorSample.Web.Commands;
using RazorSample.Web.Extensions;
using RazorSample.Web.Queries;
using RazorSample.Web.Services;
using System;
using System.Threading.Tasks;

namespace RazorSample.Web.Controllers
{
  public sealed class EmployeeController : Controller
  {
    private readonly IResourceBuilder _builder;
    private readonly IEmployeeService _employeeService;
    private readonly IRandomGenerator _randomGenerator;

    public EmployeeController(IResourceBuilder builder, IEmployeeService employeeService, IRandomGenerator randomGenerator)
    {
      _builder = builder ?? throw new ArgumentNullException(nameof(builder));
      _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
      _randomGenerator = randomGenerator ?? throw new ArgumentNullException(nameof(randomGenerator));
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      _builder.Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Client", "index", "client"))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Action, "new employee", nameof(Add), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Self, "Employees", nameof(Index), nameof(EmployeeController)));

      var employees = await _employeeService.HandleAsync(new SearchEmployeesQuery());

      if (employees.HasError == false)
      {
        foreach (var employee in employees.Result)
        {
          _builder.Embedded(RelTypes.Row)
                  .Property(new Property(nameof(employee.FullName), "Name", employee.FullName))
                  .Property(new Property(nameof(employee.EmployeeNo), "Employee No", employee.EmployeeNo))
                  .Property(new Property(nameof(employee.Created), "Created", employee.Created))
                  .Link(Url.AppLink(RelTypes.Self, "Name", nameof(Edit), nameof(EmployeeController), new UpdateEmployeeQuery(employee.EmployeeId)));
        }
      }

      var vm = _builder.Build()
                       .ToListVm();

      return View("GridView", vm);
    }

    [HttpGet]
    public IActionResult Add()
    {
      var command = _randomGenerator.RandomEmployee();
      var vm = BuildAddFormVm(command);

      return View("FormView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateEmployeeCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var vm = BuildAddFormVm(command);

        return View("FormView", vm);
      }

      await _employeeService.HandleAsync(command);

      return RedirectToAction(nameof(Index), new SearchEmployeesQuery(command.EmployeeNo));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(UpdateEmployeeQuery query)
    {
      var command = await _employeeService.HandleAsync(query);
      var vm = BuildEditFormVm(command.Result);

      return View("FormView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateEmployeeQuery query, UpdateEmployeeCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var vm = BuildEditFormVm(command);

        return View("FormView", vm);
      }

      await _employeeService.HandleAsync(command);

      return RedirectToAction(nameof(Index), new SearchEmployeesQuery(command.EmployeeNo));
    }

    private IFormVm BuildAddFormVm(EmployeeFormCommandBase command) =>
      BuildFormBase(command).Link(Url.AppLink(RelTypes.Self, "Employees", nameof(Index), nameof(EmployeeController)))
                            .Link(Url.AppLink(RelTypes.Breadcrumb, "New Employee", nameof(Add), nameof(EmployeeController)))
                            .Build()
                            .ToFormVm();

    private IFormVm BuildEditFormVm(EmployeeFormCommandBase command) =>
      BuildFormBase(command).Link(Url.AppLink(RelTypes.Self, "Employees", nameof(Index), nameof(EmployeeController)))
                            .Link(Url.AppLink(RelTypes.Breadcrumb, $"{command.LastName}, {command.FirstName}", nameof(Edit), nameof(EmployeeController)))
                            .Link(Url.AppLink(RelTypes.Action, "+ new employee", nameof(EmployeeController.Add), nameof(EmployeeController)))
                            .Build()
                            .ToFormVm();

    private IResourceBuilder BuildFormBase(EmployeeFormCommandBase command) =>
      _builder.Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Client", "index", "client"))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Employees", nameof(Index), nameof(EmployeeController)))
              .Property(new Property(nameof(command.FirstName), "First Name", command.FirstName))
              .Property(new Property(nameof(command.LastName), "Last Name", command.LastName))
              .Property(new Property(nameof(command.EmployeeNo), "Employee No", command.EmployeeNo))
              .Property(new Property(nameof(command.Email), "Email", command.Email));
  }
}
