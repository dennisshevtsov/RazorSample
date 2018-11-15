using RazorSample.Hr;
using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IFormVm : IPageVm
  {
    IEnumerable<IInputVm> Inputs { get; }
    IEnumerable<ISelectVm> Selects { get; }

    Link Self { get; }

    IEnumerable<Link> Tabs { get; }
    bool HasTabs { get; }
  }
}
