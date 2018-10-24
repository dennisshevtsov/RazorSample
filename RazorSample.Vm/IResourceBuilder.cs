namespace RazorSample.Vm
{
  public interface IResourceBuilder
  {
    IResourceBuilder Link(Link link);

    IResource Build();
  }
}
