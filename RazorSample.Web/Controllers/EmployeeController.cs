using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index(SearchEmployeesQuery query)
        {
            var vm = new EmployeeListVm().Controller(this)
                                         .Query(query)
                                         .Items(await _employeeService.HandleAsync(query));

            return View("ListView", vm);
        }
    }
}
