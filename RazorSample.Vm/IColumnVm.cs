using RazorSample.Hr;

namespace RazorSample.Vm
{
  public interface IColumnVm
  {
    string Name { get; }
    string DisplayName { get; }

    Link Search { get; }
  }
}
