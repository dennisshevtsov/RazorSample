using RazorSample.Web.Commands;
using System;
using System.Collections.Generic;

namespace RazorSample.Web.ViewModels
{
  public sealed class ClientTeamMemberVm : IClientTeamMember
  {
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; }

    public Guid RoleId { get; set; }
  }

  public sealed class ClientTeamVm : IClientTeam<ClientTeamMemberVm>
  {
    public IEnumerable<ClientTeamMemberVm> Members { get; internal set; }
  }

  public sealed class ClientTeamFormVm
  {
    public ClientTeamVm Command { get; set; }

    public IReadOnlyDictionary<Guid, string> Employees { get; set; }
    public IReadOnlyDictionary<Guid, string> Roles { get; set; }
  }
}
