using Microsoft.AspNetCore.Mvc;
using RazorSample.Vm;
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

    public EmployeeController(IResourceBuilder builder, IEmployeeService employeeService)
    {
      _builder = builder ?? throw new ArgumentNullException(nameof(builder));
      _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
    }

    public async Task<IActionResult> Index()
    {
      _builder.Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Client", "index", "client"))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Employees", nameof(Index), nameof(EmployeeController)));

      var employees = await _employeeService.HandleAsync(new SearchEmployeesQuery());

      if (employees.HasError == false)
      {
        foreach (var employee in employees.Result)
        {
          _builder.Embedded(RelTypes.List)
                  .Property(new Property(nameof(employee.FullName), "Name", employee.FullName))
                  .Property(new Property(nameof(employee.EmployeeNo), "Employee No", employee.EmployeeNo))
                  .Property(new Property(nameof(employee.Created), "Created", employee.Created))
                  .Link(Url.AppLink(RelTypes.Self, "Name", "edit", nameof(EmployeeController)));
        }
      }

      var resource = _builder.Build();
      var vm = new ListVm(resource);

      vm.Title = "Employees";

      return View("ListView", vm);
    }
  }
}
