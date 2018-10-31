using RazorSample.Hr;
using System;

namespace RazorSample.Vm
{
  public static class VmExtensions
  {
    public static IListVm ToListVm(this IResource source)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      return new ListVm(source);
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
