using System;

namespace RazorSample.Vm
{
  public sealed class ResourceBuilder : IResourceBuilder
  {
    private readonly BuildingResource _resource;

    public ResourceBuilder() : this(new BuildingResource()) { }

    internal ResourceBuilder(BuildingResource resource)
    {
      _resource = resource ?? throw new ArgumentNullException(nameof(resource));
    }

    public IResource Build() => _resource;

    public IResourceBuilder Link(Link link)
    {
      _resource.AddLink(link);

      return this;
    }

    public IResourceBuilder Property(Property property)
    {
      _resource.AddProperty(property);

      return this;
    }

    public IResourceBuilder Embedded(string rel)
    {
      var resource = new BuildingResource();
      _resource.AddEmbedded(rel, resource);

      return new ResourceBuilder(resource);
    }
  }
}
