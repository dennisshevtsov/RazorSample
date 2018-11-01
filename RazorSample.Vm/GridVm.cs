using RazorSample.Hr;
using System.Collections.Generic;
using System.Linq;

namespace RazorSample.Vm
{
  public sealed class GridVm : VmBase, IGridVm
  {
    public GridVm(IResource resource) : base(resource) { }

    public IEnumerable<Column> Columns => _resource.Embedded.FirstOrDefault(resource => resource.Key == RelTypes.Row)
                                                            .Value?.Properties.Select(property => new Column(property.Name,
                                                                                                             property.DisplayName));

    public IEnumerable<IVm> Rows => _resource.Embedded.Where(resource => resource.Key == RelTypes.Row)
                                                      .Select(resource => new Vm(resource.Value));

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
