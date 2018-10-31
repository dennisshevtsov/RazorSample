using RazorSample.Hr;
using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IGridVm<TItem> : IPageVm where TItem : IVm
  {
    IEnumerable<Column> Columns { get; }
    IEnumerable<TItem> Rows { get; }

    Link FirstPage { get; }
    Link PrevPage { get; }
    Link NextPage { get; }
    Link LastPage { get; }
  }

  public interface IGridVm : IGridVm<IVm> { }
}
