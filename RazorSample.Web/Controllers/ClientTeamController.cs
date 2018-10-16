using Microsoft.AspNetCore.Mvc;
using RazorSample.Web.Commands;
using RazorSample.Web.Queries;
using RazorSample.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorSample.Web.Controllers
{
  public sealed class ClientTeamController : Controller
  {
    private readonly ICommandHandler<UpdateClientTeamCommand> _updateClientTeamCommandHandler;

    [HttpGet]
    public IActionResult Index(UpdateClientTeamQuery query)
    {
      var vm = new ClientTeamFormVm();

      vm.Command = new ClientTeamVm();
      vm.Command.Members = new ClientTeamMemberVm[0];

      vm.Employees = new Dictionary<Guid, string>();
      vm.Roles = new Dictionary<Guid, string>();

      return View("ClientTeamView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Index(UpdateClientTeamQuery query, UpdateClientTeamCommand command)
    {
      await _updateClientTeamCommandHandler.HandleAsync(command);

      return RedirectToAction(nameof(ClientController.Index),
                              nameof(ClientController).Replace("controller", "", StringComparison.InvariantCultureIgnoreCase),
                              (ISearchClientsQuery)query);
    }
  }
}
