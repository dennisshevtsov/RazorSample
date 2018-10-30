namespace RazorSample.Vm
{
  public interface IFormVm<TForm> : IPageVm where TForm : IVm
  {
    TForm Form { get; }
  }

  public interface IFormVm : IFormVm<IVm> { }
}
