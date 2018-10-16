using RazorSample.Web.Descriptors;
using RazorSample.Web.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RazorSample.Web.Commands
{
  public interface IClientTeamMember : IUpdateEmployeeQuery
  {
    Guid RoleId { get; set; }
  }

  public interface IClientTeam<out TClienTeamMember> where TClienTeamMember : IClientTeamMember
  {
    IEnumerable<TClienTeamMember> Members { get; }
  }

  public class ClientTeamMember : IClientTeamMember
  {
    [Required]
    public Guid EmployeeId { get; set; }

    [Required]
    public Guid RoleId { get; set; }
  }

  public sealed class UpdateClientTeamCommand : IUpdateClientQuery, IClientTeam<ClientTeamMember>
  {
    [Required]
    public Guid ClientId { get; set; }

    public IEnumerable<ClientTeamMember> Members { get; set; }
  }
}
