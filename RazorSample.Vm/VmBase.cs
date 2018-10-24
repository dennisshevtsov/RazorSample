using System.Collections.Generic;
using System.Linq;

namespace RazorSample.Vm
{
  public abstract class VmBase : IVm, IResource
  {
    public string Title { get; internal set; }

    public IEnumerable<Link> Breadcrumbs => Links.Where(link => link.Rel == RelTypes.Breadcrumb);

    public IEnumerable<Link> Navs => Links.Where(link => link.Rel == RelTypes.Nav);

    public IEnumerable<Link> Actions => Links.Where(link => link.Rel == RelTypes.Action);

    public IEnumerable<Link> Links { get; internal set; }
  }
}
