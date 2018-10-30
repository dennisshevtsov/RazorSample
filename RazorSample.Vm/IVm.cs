using System;
using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IVm
  {
    IEnumerable<Property> Properties { get; }
    IEnumerable<Link> Actions { get; }
    
    Link Self { get; }
  }

  public interface IPageVm //: IVm
  {
    string Title { get; }

    IEnumerable<Link> Navs { get; }
    IEnumerable<Link> Breadcrumbs { get; }
  }

  public sealed class Column
  {
    public Column(string name, string displayName)
    {
      Name = name ?? throw new ArgumentNullException(nameof(name));
      DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
    }

    public string Name { get; }
    public string DisplayName { get; }
  }

  public interface IListVm<TItem> : IPageVm where TItem : IVm
  {
    IEnumerable<Column> Columns { get; }
    IEnumerable<TItem> Rows { get; }
  }

  public interface IListVm : IListVm<IVm> { }

  public interface IFormVm<TForm> : IPageVm where TForm : IVm
  {
    TForm Form { get; }
  }
}
