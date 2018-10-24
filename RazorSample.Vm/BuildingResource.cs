using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RazorSample.Vm
{
  internal sealed class BuildingResource : IResource
  {
    private readonly ICollection<Link> _links;

    internal BuildingResource()
    {
      _links = new Collection<Link>();
    }

    public IEnumerable<Link> Links => _links;

    internal void AddLink(Link link)
    {
      _links.Add(link);
    }
  }
}
