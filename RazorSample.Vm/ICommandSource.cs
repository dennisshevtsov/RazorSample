namespace RazorSample.Vm
{
  public interface ICommandSource<TCommand> where TCommand : class
  {
    TCommand Command { get; }

    Link OkAction { get; }
    Link CancelAction { get; }
  }
}
