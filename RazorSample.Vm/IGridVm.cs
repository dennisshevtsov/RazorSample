using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IGridVm : IPageVm, IPagingSource
  {
    IEnumerable<IColumnVm> Columns { get; }
    IEnumerable<IRowVm> Rows { get; }

    bool HasData { get; }
  }
}
