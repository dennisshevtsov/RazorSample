using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IPageVm
  {
    string Title { get; }

    IEnumerable<Link> Navs { get; }
    IEnumerable<Link> Breadcrumbs { get; }
  }
}
