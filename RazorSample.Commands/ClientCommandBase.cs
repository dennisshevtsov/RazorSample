using System;
using System.ComponentModel.DataAnnotations;

namespace RazorSample.Commands
{
  public abstract class ClientCommandBase
  {
    public Guid ClientId { get; set; }

    public string ClientNo { get; set; }
    public string OrganizationNo { get; set; }

    public string Name { get; set; }

    [Required]
    public Guid? ClientOwnerId { get; set; }
  }
}
