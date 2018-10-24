using System;

namespace RazorSample.Vm
{
  public static class VmExtensions
  {
    public static IListVm<TItem> ToListVm<TItem>(this IResource source) where TItem : class
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      return new ListVm<TItem> { Links = source.Links, };
    }

    public static IFormVm<TItem> ToFormVm<TItem>(this IResource source) where TItem : class
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      return new FormVm<TItem> { Links = source.Links, };
    }
  }
}
