namespace RazorSample.Web.ViewModels
{
  public interface ICommandSource<out TCommand> where TCommand : class
  {
    TCommand Command { get; }
  }

  public interface ICommandSourceInternal<TCommand> where TCommand : class
  {
    TCommand Command { get; set; }
  }

  public abstract class FormVmBase<TQuery, TCommand> : VmBase<TQuery>, ICommandSource<TCommand>, ICommandSourceInternal<TCommand>
      where TQuery : class
      where TCommand : class
  {
    public TCommand Command { get; internal set; }
    TCommand ICommandSourceInternal<TCommand>.Command { get { return Command; } set { Command = value; } }
  }

  public abstract class FormVmBase<TCommand> : VmBase, ICommandSource<TCommand>, ICommandSourceInternal<TCommand>
    where TCommand : class
  {
    public TCommand Command { get; internal set; }
    TCommand ICommandSourceInternal<TCommand>.Command { get { return Command; } set { Command = value; } }
  }
}
