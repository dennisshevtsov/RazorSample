using RazorSample.Hr;
using System;

namespace RazorSample.Vm
{
  public sealed class InputVm : IInputVm
  {
    private readonly Property _property;

    public InputVm(Property property)
    {
      _property = property ?? throw new ArgumentNullException(nameof(property));
    }

    public string Name => _property.Name;

    public string DisplayName => _property.DisplayName;

    public object Value => _property.Value;
  }
}
