using RazorSample.Hr;
using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface ISelectVm : IInputVm
  {
    string SearchName { get; }
    string SearchValue { get; }

    Link Search { get; }
    Link Clear { get; }

    IEnumerable<Link> Options { get; }
    bool HasOptions { get; }

    bool IsEmpty { get; }
  }
}
