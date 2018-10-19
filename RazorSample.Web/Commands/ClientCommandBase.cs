using System;

namespace RazorSample.Web.Commands
{
  public abstract class ClientCommandBase
  {
    public Guid ClientId { get; set; }

    public string ClientNo { get; set; }
    public string OrganizationNo { get; set; }

    public string Name { get; set; }
  }
}
