using RazorSample.Hr;
using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface ISelectVm : IInputVm
  {
    string DisplayValue { get; }

    Link Search { get; }

    IEnumerable<Link> Options { get; }
    bool HasOptions { get; }
  }
}
