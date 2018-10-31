using Microsoft.AspNetCore.Mvc;
using RazorSample.Web.Commands;
using RazorSample.Web.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorSample.Web.Controllers
{
    //public sealed class ClientTeamController : Controller
    //{
    //    [HttpGet]
    //    public IActionResult Index(UpdateClientTeamQuery query)
    //    {
    //        var vm = new ClientTeamFormVm();

    //        vm.Command = new ClientTeamVm();
    //        vm.Command.Members = new ClientTeamMemberVm[0];

    //        vm.Employees = new Dictionary<Guid, string>();
    //        vm.Roles = new Dictionary<Guid, string>();

    //        return View("ClientTeamView", vm);
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Index(UpdateClientTeamCommand command)
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }


    //    public IActionResult Employees(SearchEmployeesQuery query)
    //    {
    //        var vm = new EmployeeListVm();

    //        return View("EmployeeListView", vm);
    //    }

    //    public IActionResult Employees(AddEmployeeCommand command)
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //}
}
