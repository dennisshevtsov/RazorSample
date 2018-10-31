using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorSample.Vm
{
  public abstract class VmBase : IPageVm
  {
    protected readonly IResource _resource;

    protected VmBase(IResource resource)
    {
      _resource = resource ?? throw new ArgumentNullException(nameof(resource));
    }

    private string _title;
    public string Title => _title ?? (_title = _resource.Links.Single(link => link.Rel == RelTypes.Self).Title);

    private IEnumerable<Link> _nav;
    public IEnumerable<Link> Navs => _nav ?? (_nav = _resource.Links.Where(link => link.Rel == RelTypes.Nav));

    private IEnumerable<Link> _breadcrumbs;
    public IEnumerable<Link> Breadcrumbs => _breadcrumbs ?? (_breadcrumbs = _resource.Links.Where(link => link.Rel == RelTypes.Breadcrumb));

    private Link _selectedNav;
    public Link SelectedNav => _selectedNav ?? (_selectedNav = Navs.First());
  }
}
