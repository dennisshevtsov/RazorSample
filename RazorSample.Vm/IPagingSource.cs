using RazorSample.Hr;

namespace RazorSample.Vm
{
  public interface IPagingSource
  {
    Link FirstPage { get; }
    Link PrevPage { get; }
    Link NextPage { get; }
    Link LastPage { get; }
  }
}
