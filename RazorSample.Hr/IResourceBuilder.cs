namespace RazorSample.Hr
{
  public interface IResourceBuilder
  {
    IResourceBuilder Link(Link link);
    IResourceBuilder Property(Property property);
    IResourceBuilder Embedded(string rel);

    IResource Build();
  }
}
