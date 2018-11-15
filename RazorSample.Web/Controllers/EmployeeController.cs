using Microsoft.AspNetCore.Mvc;
using RazorSample.Hr;
using RazorSample.Random;
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
    public async Task<IActionResult> Index(SearchEmployeeQuery query)
    {
      _builder.Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Client", "index", "client"))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Action, "+ new employee", nameof(Add), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Self, "Employees", nameof(Index), nameof(EmployeeController)));

      var employees = await _employeeService.HandleAsync(query);

      if (employees.HasError == false)
      {
        if (employees.Result.PageNo > employees.Result.FirstPageNo)
        {
          _builder.Link(Url.AppLink(RelTypes.First, "First", nameof(Index), nameof(EmployeeController), new SearchEmployeeQuery(query.EmployeeNo, employees.Result.FirstPageNo)))
                  .Link(Url.AppLink(RelTypes.Prev, "Previous", nameof(Index), nameof(EmployeeController), new SearchEmployeeQuery(query.EmployeeNo, employees.Result.PageNo - 1)));
        }

        if (employees.Result.PageNo < employees.Result.LastPageNo)
        {
          _builder.Link(Url.AppLink(RelTypes.Next, "Next", nameof(Index), nameof(EmployeeController), new SearchEmployeeQuery(query.EmployeeNo, employees.Result.PageNo + 1)))
                  .Link(Url.AppLink(RelTypes.Last, "Last", nameof(Index), nameof(EmployeeController), new SearchEmployeeQuery(query.EmployeeNo, employees.Result.LastPageNo)));
        }

        foreach (var employee in employees.Result)
        {
          _builder.Embedded(RelTypes.Row)
                  .Property("name", "Name", employee.FullName)
                  .Property(nameof(employee.EmployeeNo), "Employee No", employee.EmployeeNo)
                  .Property(nameof(employee.Created), "Created", employee.Created)
                  .Link(Url.AppLink(RelTypes.Self, "Name", nameof(Edit), nameof(EmployeeController), new UpdateEmployeeQuery(employee.EmployeeId)))
                  .Link(Url.AppLink(RelTypes.Action, "+ new client", nameof(ClientController.Add), nameof(ClientController), new CreateClientQuery(employee.EmployeeId)));
        }
      }

      var vm = _builder.Build()
                       .ToGridVm();

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

      return RedirectToAction(nameof(Index), new SearchEmployeeQuery(command.EmployeeNo));
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

      return RedirectToAction(nameof(Index), new SearchEmployeeQuery(command.EmployeeNo));
    }

    private IFormVm BuildAddFormVm(EmployeeFormCommandBase command) =>
      BuildFormBase(command).Link(Url.AppLink(RelTypes.Self, "Employees", nameof(Index), nameof(EmployeeController)))
                            .Link(Url.AppLink(RelTypes.Breadcrumb, "New Employee", nameof(Add), nameof(EmployeeController)))
                            .Build()
                            .ToFormVm();

    private IFormVm BuildEditFormVm(UpdateEmployeeCommand command) =>
      BuildFormBase(command).Link(Url.AppLink(RelTypes.Self, "Employees", nameof(Edit), nameof(EmployeeController), new UpdateEmployeeQuery(command.EmployeeId)))
                            .Link(Url.AppLink(RelTypes.Breadcrumb, $"{command.LastName}, {command.FirstName}", nameof(Edit), nameof(EmployeeController)))
                            .Link(Url.AppLink(RelTypes.Action, "+ new employee", nameof(EmployeeController.Add), nameof(EmployeeController)))
                            //.Property(nameof(command.Phone), "Phone number", command.Phone)
                            //.Property(nameof(command.Address), "Address", command.Address)
                            .Link(Url.AppLink(RelTypes.Tab, "General Info", nameof(Edit), nameof(EmployeeController), new UpdateEmployeeQuery(command.EmployeeId)))
                            .Link(Url.AppLink(RelTypes.Tab, "Addresses", nameof(Index), nameof(EmployeeController)))
                            .Link(Url.AppLink(RelTypes.Tab, "Emails", nameof(Index), nameof(EmployeeController)))
                            .Link(Url.AppLink(RelTypes.Tab, "Phones", nameof(Index), nameof(EmployeeController)))
                            .Link(Url.AppLink(RelTypes.Tab, "IM", nameof(Index), nameof(EmployeeController)))
                            .Build()
                            .ToFormVm();

    private IResourceBuilder BuildFormBase(EmployeeFormCommandBase command) =>
      _builder.Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Client", "index", "client"))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Employees", nameof(Index), nameof(EmployeeController)))
              .Property(nameof(command.FirstName), "First Name", command.FirstName)
              .Property(nameof(command.LastName), "Last Name", command.LastName)
              .Property(nameof(command.EmployeeNo), "Employee No", command.EmployeeNo)
              .Property(nameof(command.Email), "Email", command.Email);
  }
}
