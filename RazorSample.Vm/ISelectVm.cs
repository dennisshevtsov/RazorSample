using RazorSample.Hr;

namespace RazorSample.Vm
{
  public interface ISelectVm : IInputVm
  {
    string DisplayValue { get; }

    Link Search { get; }
  }
}
