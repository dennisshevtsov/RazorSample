using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IListVm<TItem> : IPageVm where TItem : IVm
  {
    IEnumerable<Column> Columns { get; }
    IEnumerable<TItem> Rows { get; }
  }

  public interface IListVm : IListVm<IVm> { }
}
