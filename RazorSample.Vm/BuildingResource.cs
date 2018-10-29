using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RazorSample.Vm
{
  internal sealed class BuildingResource : IResource
  {
    private readonly ICollection<Link> _links;
    private readonly ICollection<Property> _properties;
    private readonly ICollection<KeyValuePair<string, IResource>> _embedded;

    internal BuildingResource()
    {
      _links = new Collection<Link>();
    }

    public IEnumerable<Link> Links => _links;

    public IEnumerable<Property> Properties => _properties;

    public IEnumerable<KeyValuePair<string, IResource>> Embedded => _embedded;

    internal void AddLink(Link link)
    {
      _links.Add(link);
    }

    internal void AddProperty(Property property)
    {
      _properties.Add(property);
    }

    internal void AddEmbedded(string rel, IResource resource)
    {
      _embedded.Add(new KeyValuePair<string, IResource>(rel, resource));
    }
  }
}
