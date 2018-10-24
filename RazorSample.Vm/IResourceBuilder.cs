namespace RazorSample.Vm
{
  interface IResourceBuilder
  {
    IResourceBuilder Link(Link link);

    IResource Build();
  }
}
