using RazorSample.Hr;
using System.Collections.Generic;
using System.Linq;

namespace RazorSample.Vm
{
  public sealed class ListVm : VmBase, IListVm
  {
    public ListVm(IResource resource) : base(resource) { }

    public IEnumerable<Column> Columns => _resource.Embedded.FirstOrDefault(resource => resource.Key == RelTypes.Row)
                                                            .Value?.Properties.Select(property => new Column(property.Name,
                                                                                                             property.DisplayName));

    public IEnumerable<IVm> Rows => _resource.Embedded.Where(resource => resource.Key == RelTypes.Row)
                                                      .Select(resource => new ListItemVm(resource.Value));
  }
}
