using System;
using System.Collections.Generic;
using System.Linq;
using RazorSample.Hr;

namespace RazorSample.Vm
{
  public sealed class SelectVm : ISelectVm
  {
    private readonly Property _property;
    private readonly Link _search;
    private readonly Link[] _options;

    public SelectVm(IResource resource)
    {
      if (resource == null)
      {
        throw new ArgumentNullException(nameof(resource));
      }

      _property = resource.Properties.Single();
      _search = resource.Links.Single(link => link.Rel == RelTypes.Search);
      _options = resource.Links.Where(link => link.Rel == RelTypes.Action).ToArray();
    }

    public string Name => _property.Name;
    public string DisplayName => _property.DisplayName;

    public object Value => _property.Value;
    public string DisplayValue => _property.DisplayValue;

    public Link Search => _search;

    public IEnumerable<Link> Options => _options;
    public bool HasOptions => _options.Length > 0;
  }
}
