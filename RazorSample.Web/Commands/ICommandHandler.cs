using System.Threading.Tasks;

namespace RazorSample.Web.Commands
{
  public interface ICommandHandler<TCommand> where TCommand : class
  {
    Task<CommandExecutionResult> HandleAsync(TCommand command);
  }
}
