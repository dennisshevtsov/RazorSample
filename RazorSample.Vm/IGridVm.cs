using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IGridVm<TItem> : IPageVm, IPagingSource where TItem : IVm
  {
    IEnumerable<Column> Columns { get; }
    IEnumerable<TItem> Rows { get; }
  }

  public interface IGridVm : IGridVm<IVm> { }
}
