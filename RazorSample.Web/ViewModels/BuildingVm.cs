using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorSample.Web.ViewModels
{
  public sealed class BuildingVm<TQuery, TItem> : ListVmBase<TQuery, TItem>
    where TQuery : class
    where TItem : class
  { }

  public interface IVmBuilder<TQuery, TItem>
    where TQuery : class
    where TItem : class
  {
    ListVmBase<TQuery, TItem> Build();

    IVmBuilder<TQuery, TItem> Title(string title);
    IVmBuilder<TQuery, TItem> Nav(Link link);
    IVmBuilder<TQuery, TItem> Breadcrumb(Link link);
    IVmBuilder<TQuery, TItem> Action(Link link);
  }

  public sealed class VmBuilder<TQuery, TItem> : IVmBuilder<TQuery, TItem>
    where TQuery : class
    where TItem : class
  {
    private readonly IList<Link> _navs;
    private readonly IList<Link> _breadcrumbs;
    private readonly IList<Link> _actions;

    private string _title;

    public VmBuilder()
    {
      _navs = new List<Link>();
      _breadcrumbs = new List<Link>();
      _actions = new List<Link>();
    }

    public ListVmBase<TQuery, TItem> Build()
    {
      var vm = new BuildingVm<TQuery, TItem>
      {
        Title = _title,
        Navs = _navs,
        Breadcrumbs = _breadcrumbs,
        Actions = _actions,
      };

      return vm;
    }

    public IVmBuilder<TQuery, TItem> Title(string title)
    {
      _title = title;

      return this;
    }

    public IVmBuilder<TQuery, TItem> Nav(Link link)
    {
      _navs.Add(link);

      return this;
    }

    public IVmBuilder<TQuery, TItem> Breadcrumb(Link link)
    {
      _breadcrumbs.Add(link);

      return this;
    }

    public IVmBuilder<TQuery, TItem> Action(Link link)
    {
      _actions.Add(link);

      return this;
    }
  }

  public interface IFormVmBuilder<TQuery, TCommand>
    where TQuery : class
    where TCommand : class
  {
    FormVmBase<TQuery, TCommand> Build();

    IFormVmBuilder<TQuery, TCommand> Title(string title);
    IFormVmBuilder<TQuery, TCommand> Nav(Link link);
    IFormVmBuilder<TQuery, TCommand> Breadcrumb(Link link);
    IFormVmBuilder<TQuery, TCommand> Action(Link link);
  }

  public sealed class FormVm<TQuery, TCommand> : FormVmBase<TQuery, TCommand>
    where TQuery : class
    where TCommand : class
  { }

  public sealed class FormVmBuilder<TQuery, TCommand> : IFormVmBuilder<TQuery, TCommand>
    where TQuery : class
    where TCommand : class
  {
    private readonly IList<Link> _navs;
    private readonly IList<Link> _breadcrumbs;
    private readonly IList<Link> _actions;

    private string _title;

    public FormVmBuilder()
    {
      _navs = new List<Link>();
      _breadcrumbs = new List<Link>();
      _actions = new List<Link>();
    }

    public FormVmBase<TQuery, TCommand> Build()
    {
      var vm = new FormVm<TQuery, TCommand>
      {
        Title = _title,
        Navs = _navs,
        Breadcrumbs = _breadcrumbs,
        Actions = _actions,
      };

      return vm;
    }

    public IFormVmBuilder<TQuery, TCommand> Title(string title)
    {
      _title = title;

      return this;
    }

    public IFormVmBuilder<TQuery, TCommand> Nav(Link link)
    {
      _navs.Add(link);

      return this;
    }

    public IFormVmBuilder<TQuery, TCommand> Breadcrumb(Link link)
    {
      _breadcrumbs.Add(link);

      return this;
    }

    public IFormVmBuilder<TQuery, TCommand> Action(Link link)
    {
      _actions.Add(link);

      return this;
    }
  }

  public interface IFormVmBuilder<TCommand>
    where TCommand : class
  {
    FormVmBase<TCommand> Build();

    IFormVmBuilder<TCommand> Title(string title);
    IFormVmBuilder<TCommand> Nav(Link link);
    IFormVmBuilder<TCommand> Breadcrumb(Link link);
    IFormVmBuilder<TCommand> Action(Link link);
  }

  public sealed class FormVm<TCommand> : FormVmBase<TCommand>
    where TCommand : class
  { }

  public sealed class FormVmBuilder<TCommand> : IFormVmBuilder<TCommand>
    where TCommand : class
  {
    private readonly IList<Link> _navs;
    private readonly IList<Link> _breadcrumbs;
    private readonly IList<Link> _actions;

    private string _title;

    public FormVmBuilder()
    {
      _navs = new List<Link>();
      _breadcrumbs = new List<Link>();
      _actions = new List<Link>();
    }

    public FormVmBase<TCommand> Build()
    {
      var vm = new FormVm<TCommand>
      {
        Title = _title,
        Navs = _navs,
        Breadcrumbs = _breadcrumbs,
        Actions = _actions,
      };

      return vm;
    }

    public IFormVmBuilder<TCommand> Title(string title)
    {
      _title = title;

      return this;
    }

    public IFormVmBuilder<TCommand> Nav(Link link)
    {
      _navs.Add(link);

      return this;
    }

    public IFormVmBuilder<TCommand> Breadcrumb(Link link)
    {
      _breadcrumbs.Add(link);

      return this;
    }

    public IFormVmBuilder<TCommand> Action(Link link)
    {
      _actions.Add(link);

      return this;
    }
  }
}

