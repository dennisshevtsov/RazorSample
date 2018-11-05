namespace RazorSample.Web.Commands
{
  public sealed class UpdateClientCommand : ClientCommandBase
  {
    public string Phone { get; set; }
    public string Address { get; set; }
  }
}
