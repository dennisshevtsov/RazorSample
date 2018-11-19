using RazorSample.Hr;
using System;

namespace RazorSample.Vm
{
  public sealed class CellVm : ICellVm
  {
    private readonly Property _property;

    public CellVm(Property property)
    {
      _property = property ?? throw new ArgumentNullException(nameof(property));
    }

    public string Name => _property.Name;

    public object Value => _property.Value;
  }
}
