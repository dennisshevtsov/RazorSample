using System.Threading.Tasks;

namespace RazorSample.Commands
{
  public interface ICommandHandler<TCommand> where TCommand : class
  {
    Task<CommandExecutionResult> HandleAsync(TCommand command);
  }
}
