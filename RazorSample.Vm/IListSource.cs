using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IListSource<TItem> where TItem : class
  {
    IEnumerable<TItem> Items { get; }
  }
}
