namespace RazorSample.Vm
{
  public sealed class ResourceBuilder : IResourceBuilder
  {
    private readonly BuildingResource _resource;

    public ResourceBuilder()
    {
      _resource = new BuildingResource();
    }

    public IResource Build() => _resource;

    public IResourceBuilder Link(Link link)
    {
      _resource.AddLink(link);

      return this;
    }
  }
}
