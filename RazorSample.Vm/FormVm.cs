using RazorSample.Hr;

namespace RazorSample.Vm
{
  public sealed class FormVm : VmBase, IFormVm
  {
    public FormVm(IResource resource) : base(resource) { }

    public IVm Form => new Vm(_resource);
  }
}
