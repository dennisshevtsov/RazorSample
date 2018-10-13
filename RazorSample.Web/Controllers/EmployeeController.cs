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

    public EmployeeController(IEmployeeService employeeService)
    {
      _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
    }

    [HttpGet]
    public async Task<IActionResult> Index(SearchEmployeesQuery query)
    {
      var vm = new EmployeeListVm().Controller(this)
                                   .Query(query)
                                   .Items(await _employeeService.HandleAsync(query));

      return View("ListView", vm);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(GetEmployeeQuery query)
    {
      var vm = new EmployeeVm().Controller(this)
                               .Query(query)
                               .Command(await _employeeService.HandleAsync(query));

      return View("EditView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(GetEmployeeQuery query, UpdateEmployeeCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var vm = new EmployeeVm().Query(query)
                                 .Command(command);

        return View("EditView", vm);
      }

      await _employeeService.HandleAsync(command);

      return RedirectToAction(nameof(Edit), query);
    }
  }
}
