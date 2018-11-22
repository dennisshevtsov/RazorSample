using RazorSample.Hr;
using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IRowVm
  {
    IEnumerable<ICellVm> Properties { get; }

    IEnumerable<Link> Actions { get; }
    IEnumerable<Link> Navs { get; }

    Link Self { get; }
  }
}
