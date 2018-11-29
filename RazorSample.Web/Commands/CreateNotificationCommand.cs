using System;

namespace RazorSample.Web.Commands
{
  public sealed class CreateNotificationCommand
  {
    public CreateNotificationCommand() { }

    public CreateNotificationCommand(Guid subjectId, string title)
    {
      SubjectId = subjectId;
      Title = title ?? throw new ArgumentNullException(nameof(title));
    }

    public Guid SubjectId { get; set; }
    public string Title { get; set; }
  }
}
