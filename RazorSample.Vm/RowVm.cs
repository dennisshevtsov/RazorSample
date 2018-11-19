using RazorSample.Hr;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorSample.Vm
{
  public sealed class RowVm : IRowVm
  {
    private readonly IResource _resource;

    public RowVm(IResource resource)
    {
      _resource = resource ?? throw new ArgumentNullException(nameof(resource));
    }

    public IEnumerable<ICellVm> Properties => _resource.Properties.Select(property => new CellVm(property));

    public IEnumerable<Link> Actions => _resource.Links.Where(link => link.Rel == RelTypes.Action);

    public Link Self => _resource.Links.SingleOrDefault(link => link.Rel == RelTypes.Self);
  }
}
