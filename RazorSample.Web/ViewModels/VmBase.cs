using System.Collections.Generic;

namespace RazorSample.Web.ViewModels
{
  public abstract class VmBase
  {
    public string Title { get; internal set; }

    public IEnumerable<Link> Navs { get; internal set; }
    public string SelectedNav { get; internal set; }

    public IEnumerable<Link> Breadcrumbs { get; internal set; }

    public IEnumerable<Link> Actions { get; set; }

    public string InfoMessage { get; internal set; }
    public bool HasInfo => string.IsNullOrWhiteSpace(InfoMessage) == false;

    public string ErrorMessage { get; internal set; }
    public bool HasError => string.IsNullOrWhiteSpace(ErrorMessage) == false;
  }

  public abstract class VmBase<TQuery> : VmBase where TQuery : class
  {
    public TQuery Query { get; internal set; }
  }
}
