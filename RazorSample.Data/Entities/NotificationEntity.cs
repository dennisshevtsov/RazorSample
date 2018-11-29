using System;

namespace RazorSample.Data.Entities
{
  public sealed class NotificationEntity
  {
    public Guid NotificationId { get; set; }
    public Guid SubjectId { get; set; }

    public string Title { get; set; }

    public DateTime Created { get; set; }
    public DateTime? Closed { get; set; }

    public bool IsClosed => Closed != null;
  }
}
