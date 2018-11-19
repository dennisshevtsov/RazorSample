using RazorSample.Hr;
using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IRowVm
  {
    IEnumerable<ICellVm> Properties { get; }

    IEnumerable<Link> Actions { get; }

    Link Self { get; }
  }
}
