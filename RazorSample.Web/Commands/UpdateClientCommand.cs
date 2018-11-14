namespace RazorSample.Web.Commands
{
  public sealed class UpdateClientCommand : ClientCommandBase
  {
    public string[] Emails { get; set; }
    public string[] Phones { get; set; }
    public string[] Addresses { get; set; }
  }
}
