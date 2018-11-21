using RazorSample.Hr;
using System;

namespace RazorSample.Vm
{
  public sealed class ColumnVm : IColumnVm
  {
    public ColumnVm(string name, string displayName)
    {
      Name = name ?? throw new ArgumentNullException(nameof(name));
      DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
    }

    public ColumnVm(string name, string displayName, Link search) : this(name, displayName)
    {
      Search = search; // ?? throw new ArgumentNullException(nameof(search));
    }

    public string Name { get; }
    public string DisplayName { get; }

    public Link Search { get;}
  }
}
