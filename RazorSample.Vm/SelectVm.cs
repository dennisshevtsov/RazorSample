using System;
using System.Linq;
using RazorSample.Hr;

namespace RazorSample.Vm
{
  public sealed class SelectVm : ISelectVm
  {
    private readonly Property _property;
    private readonly Link _search;

    public SelectVm(IResource resource)
    {
      if (resource == null)
      {
        throw new ArgumentNullException(nameof(resource));
      }

      _property = resource.Properties.Single();
      _search = resource.Links.Single(link => link.Rel == RelTypes.Search);
    }

    public string Name => _property.Name;
    public string DisplayName => _property.DisplayName;

    public object Value => _property.Value;
    public string DisplayValue => _property.DisplayValue;

    public Link Search => _search;
  }
}
