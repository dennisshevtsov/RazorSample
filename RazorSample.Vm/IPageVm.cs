using RazorSample.Hr;
using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IPageVm
  {
    string Title { get; }

    IEnumerable<Link> Navs { get; }
    IEnumerable<Link> Breadcrumbs { get; }
    IEnumerable<Link> Actions { get; }

    Link SelectedNav { get; }

    IEnumerable<Link> Tabs { get; }
    bool HasTabs { get; }

    Link Self { get; }
  }
}
