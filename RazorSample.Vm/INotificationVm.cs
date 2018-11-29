using RazorSample.Hr;

namespace RazorSample.Vm
{
  public interface INotificationVm
  {
    string Title { get; }

    bool IsInfo { get; }
    bool IsError { get; }

    Link Close { get; }
  }
}
