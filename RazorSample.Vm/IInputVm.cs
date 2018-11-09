namespace RazorSample.Vm
{
  public interface IInputVm
  {
    string Name { get; }
    string DisplayName { get; }

    object Value { get; }
    string DisplayValue { get; }
  }
}
