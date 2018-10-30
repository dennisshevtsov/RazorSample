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

    public async Task<IActionResult> Index()
    {
      _builder.Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Client", "index", "client"))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Action, "new employee", nameof(Add), nameof(EmployeeController)));

      var employees = await _employeeService.HandleAsync(new SearchEmployeesQuery());

      if (employees.HasError == false)
      {
        foreach (var employee in employees.Result)
        {
          _builder.Embedded(RelTypes.Row)
                  .Property(new Property(nameof(employee.FullName), "Name", employee.FullName))
                  .Property(new Property(nameof(employee.EmployeeNo), "Employee No", employee.EmployeeNo))
                  .Property(new Property(nameof(employee.Created), "Created", employee.Created))
                  .Link(Url.AppLink(RelTypes.Self, "Name", "edit", nameof(EmployeeController)));
        }
      }

      var vm = _builder.Build()
                       .ToListVm();

      return View("ListView", vm);
    }

    public async Task<IActionResult> Add()
    {
      //var builder = new FormVmBuilder<CreateEmployeeCommand>();
      //      var vm = builder.Title("New Employee")
      //                      .Breadcrumb(Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)))
      //                      .Breadcrumb(Url.AppLink(EmployeeController.EmployeeCreateRel, "New Employee", nameof(EmployeeController.Add), nameof(EmployeeController)))
      //                      .Nav(Url.AppLink(EmployeeController.EmployeeSearchRel, "Employees", nameof(EmployeeController.Index), nameof(EmployeeController)))
      //                      .Nav(Url.AppLink(ClientController.ClientSearchRel, "Clients", nameof(ClientController.Index), nameof(ClientController)))
      //                      .Build()
      //                      .Command(_randomGenerator.RandomEmployee());

      //      vm.SelectedNav = EmployeeController.EmployeeSearchRel;

      //      return View("AddView", vm);

      _builder.Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Client", "index", "client"))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "New Employee", nameof(Add), nameof(EmployeeController)));

      var command = _randomGenerator.RandomEmployee();

      _builder.Property(new Property(nameof(command.FirstName), "First Name", command.FirstName))
              .Property(new Property(nameof(command.LastName), "Last Name", command.LastName))
              .Property(new Property(nameof(command.EmployeeNo), "Employee No", command.EmployeeNo))
              .Property(new Property(nameof(command.Email), "Email", command.Email));

      var vm = _builder.Build()
                       .ToFormVm();

      return View("AddView", vm);
    }
  }
}
