using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IListVm<TItem> : IVm, IListSource<TItem> where TItem : class { }

  public sealed class ListVm<TItem> : VmBase, IListVm<TItem> where TItem : class
  {
    public IEnumerable<TItem> Items { get; internal set; }
  }
}
