using System;
using System.Collections.Generic;
using System.Text;

namespace RazorSample.Vm
{
  public interface IListVm<TItem> : IPageVm where TItem : IVm
  {
    IEnumerable<Column> Columns { get; }
    IEnumerable<TItem> Rows { get; }
  }

  public interface IListVm : IListVm<IVm> { }
}
