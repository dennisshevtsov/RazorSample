using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IVm
  {
    string Title { get; }

    IEnumerable<Link> Navs { get; }
    IEnumerable<Link> Breadcrumbs { get; }
    IEnumerable<Link> Actions { get; }
  }
}
