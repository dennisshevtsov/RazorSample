using RazorSample.Hr;
using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IVm
  {
    IEnumerable<Property> Properties { get; }
    //IEnumerable<IInputVm> Inputs { get; }
    IEnumerable<ISelectVm> Selects { get; }

    IEnumerable<Link> Actions { get; }

    Link Self { get; }
  }
}
