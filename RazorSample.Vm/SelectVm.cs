using System;
using System.Collections.Generic;
using System.Linq;
using RazorSample.Hr;

namespace RazorSample.Vm
{
  public sealed class SelectVm : ISelectVm
  {
    private readonly IResource _resource;

    public SelectVm(IResource resource)
    {
      _resource = resource ?? throw new ArgumentNullException(nameof(resource));
    }

    private Property _inputProperty;
    private Property InputProperty => _inputProperty ?? (_inputProperty = _resource.Properties.Single());

    public string Name => InputProperty.Name;
    public string DisplayName => InputProperty.DisplayName;

    public object Value => InputProperty.Value;
    public string DisplayValue => InputProperty.DisplayValue;

    private IResource _searchResource;
    private IResource SearchResource => _searchResource ?? (_searchResource = _resource.Embedded.Single(link => link.Key == RelTypes.Search).Value);
    private Property SearchProperty => SearchResource.Properties.Single();

    public string SearchName => SearchProperty.Name;
    public string SearchValue => SearchProperty.Value?.ToString();

    public Link Search => SearchResource.Links.Single(link => link.Rel == RelTypes.Self);
    public Link Clear => _resource.Links.Single(link => link.Rel == RelTypes.Action);

    public IEnumerable<Link> Options => SearchResource.Links.Where(link => link.Rel == RelTypes.Action);
    public bool HasOptions => Options.Any();

    public bool IsEmpty => Value == null;
  }
}
