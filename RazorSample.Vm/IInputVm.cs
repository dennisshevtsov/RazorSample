using System;
using System.Collections.Generic;
using System.Text;

namespace RazorSample.Vm
{
  public interface IInputVm
  {
    string Name { get; }
    string DisplayName { get; }

    object Value { get; }
  }
}
