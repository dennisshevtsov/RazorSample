using System;

namespace RazorSample.Web.Commands
{
  public sealed class CommandExecutionResult
  {
    public CommandExecutionResult() { }

    public CommandExecutionResult(string errorMessage)
    {
      ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
    }

    public string ErrorMessage { get; }
    public bool HasError => string.IsNullOrWhiteSpace(ErrorMessage) == false;

    internal static CommandExecutionResult Success => new CommandExecutionResult();
  }
}
