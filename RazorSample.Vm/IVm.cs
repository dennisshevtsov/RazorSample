using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IVm
  {
    IEnumerable<Property> Properties { get; }
    IEnumerable<Link> Actions { get; }
  }

  public interface IPageVm //: IVm
  {
    string Title { get; }

    IEnumerable<Link> Navs { get; }
    IEnumerable<Link> Breadcrumbs { get; }
  }

  public interface IListVm<TItem> : IPageVm where TItem : IVm
  {
    IEnumerable<TItem> Items { get; }
  }

  public interface IListVm : IListVm<IVm> { }

  public interface IFormVm<TForm> : IPageVm where TForm : IVm
  {
    TForm Form { get; }
  }
}
