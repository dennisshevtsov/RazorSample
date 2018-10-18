using RazorSample.Web.Queries;
using System;

namespace RazorSample.Web.ViewModels
{
  public sealed class ClientListItemVm
  {
    public Guid ClientId { get; set; }
    public string ClientNo { get; set; }

    public string Name { get; set; }

    public DateTime Created { get; set; }
  }

  public sealed class ClientListVm : ListVmBase<SearchClientQuery, ClientListItemVm> { }
}
