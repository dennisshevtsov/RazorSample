using RazorSample.Hr;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorSample.Vm
{
  public sealed class Vm : IVm
  {
    private readonly IResource _resource;

    public Vm(IResource resource)
    {
      _resource = resource ?? throw new ArgumentNullException(nameof(resource));
    }

    public IEnumerable<Property> Properties => _resource.Properties;

    public IEnumerable<ISelectVm> Selects => _resource.Embedded.Where(resource => resource.Key == RelTypes.Select)
                                                               .Select(resource => new SelectVm(resource.Value));

    public IEnumerable<Link> Actions => _resource.Links.Where(link => link.Rel == RelTypes.Action);

    public Link Self => _resource.Links.Single(link => link.Rel == RelTypes.Self);
  }
}
