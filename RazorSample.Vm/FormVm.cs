using RazorSample.Hr;
using System.Collections.Generic;
using System.Linq;

namespace RazorSample.Vm
{
  public sealed class FormVm : VmBase, IFormVm
  {
    public FormVm(IResource resource) : base(resource) { }

    public IEnumerable<Property> Properties => _resource.Properties;

    public IEnumerable<IInputVm> Inputs => _resource.Properties.Select(property => new InputVm(property));

    public IEnumerable<ISelectVm> Selects => _resource.Embedded.Where(resource => resource.Key == RelTypes.Select)
                                                               .Select(resource => new SelectVm(resource.Value));

    public Link Self => _resource.Links.Single(link => link.Rel == RelTypes.Self);

    public IEnumerable<Link> Tabs => _resource.Links.Where(link => link.Rel == RelTypes.Tab);
    public bool HasTabs => Tabs.Any();
  }
}
