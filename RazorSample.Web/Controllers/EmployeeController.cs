using Microsoft.AspNetCore.Mvc;
using RazorSample.Data;
using RazorSample.Web.ViewModels;
using System;
using System.Threading.Tasks;

namespace RazorSample.Web.Controllers
{
    public sealed class EmployeeController : Controller
    {
        private readonly IRepository _repository;

        public EmployeeController(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IActionResult> Index()
        {
            var vm = new EmployeeListVm().Use(await _repository.PageAsync(new AllEmployeesSpecification(), 10, 0));

            return View();
        }
    }
}
