using RazorSample.Hr;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RazorSample.Vm
{
  public sealed class GridVm : VmBase, IGridVm
  {
    public GridVm(IResource resource) : base(resource) { }

    public IEnumerable<IColumnVm> Columns => _resource.Embedded.FirstOrDefault(resource => resource.Key == RelTypes.Row)
                                                               .Value?.Properties.Select(property => new ColumnVm(property.Name,
                                                                                                                  property.DisplayName,
                                                                                                                  GetColumnSearchAction(_resource, property)));
    private Link GetColumnSearchAction(IResource resource, Property property)
    {
      var pattern = $"(\\?|\\&){property.Name}\\=";
      var regex = new Regex(pattern, RegexOptions.IgnoreCase);
      var action = _resource.Links.SingleOrDefault(link => link.Rel == RelTypes.Search &&
                                                           regex.IsMatch(link.Href));

      return action;
    }

    public IEnumerable<IRowVm> Rows => _resource.Embedded.Where(resource => resource.Key == RelTypes.Row)
                                                         .Select(resource => new RowVm(resource.Value));

    public bool HasData => _resource.Embedded != null && _resource.Embedded.Any(resource => resource.Key == RelTypes.Row);

    private Link _firstPage;
    public Link FirstPage => _firstPage ?? (_firstPage = _resource.Links.SingleOrDefault(link => link.Rel == RelTypes.First));

    private Link _prevPage;
    public Link PrevPage => _prevPage ?? (_prevPage = _resource.Links.SingleOrDefault(link => link.Rel == RelTypes.Prev));

    private Link _nextPage;
    public Link NextPage => _nextPage ?? (_nextPage = _resource.Links.SingleOrDefault(link => link.Rel == RelTypes.Next));

    private Link _lastPage;
    public Link LastPage => _lastPage ?? (_lastPage = _resource.Links.SingleOrDefault(link => link.Rel == RelTypes.Last));
  }
}
