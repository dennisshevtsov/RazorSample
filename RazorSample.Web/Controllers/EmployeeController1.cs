using Microsoft.AspNetCore.Mvc;
using RazorSample.Vm;
using RazorSample.Web.Queries;
using RazorSample.Web.Services;
using System;
using System.Threading.Tasks;

namespace RazorSample.Web.Controllers
{
  public sealed class EmployeeController1 : Controller
  {
    private readonly IResourceBuilder _builder;
    private readonly IEmployeeService _employeeService;

    public EmployeeController1(IResourceBuilder builder, IEmployeeService employeeService)
    {
      _builder = builder ?? throw new ArgumentNullException(nameof(builder));
      _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
    }

    public async Task<IActionResult> Index()
    {
      _builder.Link(new Link(RelTypes.Nav, "Employees", ""))
              .Link(new Link(RelTypes.Nav, "Client", ""))
              .Link(new Link(RelTypes.Breadcrumb, "Employees", ""))
              .Property(new Property("title", "", "Employees"));

      var employees = await _employeeService.HandleAsync(new SearchEmployeesQuery());

      if (employees.HasError == false)
      {
        foreach (var employee in employees.Result)
        {
          _builder.Embedded(RelTypes.List)
                  .Property(new Property(nameof(employee.FullName), "Name", employee.FullName))
                  .Property(new Property(nameof(employee.EmployeeNo), "Employee No", employee.EmployeeNo))
                  .Property(new Property(nameof(employee.Created), "Created", employee.Created))
                  .Link(new Link(RelTypes.Self, "", ""));
        }
      }

      var resource = _builder.Build();
      //var vm = resource as IListVm<>;

      return View();
    }
  }
}
