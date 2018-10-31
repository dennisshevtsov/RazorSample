using RazorSample.Hr;
using System;

namespace RazorSample.Vm
{
  public static class VmExtensions
  {
    public static IGridVm ToGridVm(this IResource source)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      return new GridVm(source);
    }

    public static IFormVm ToFormVm(this IResource source)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      return new FormVm(source);
    }
  }
}
