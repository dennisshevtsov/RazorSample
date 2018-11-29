using System;

namespace RazorSample.Web.Queries
{
  public sealed class GetOpenNotificationsQuery
  {
    public GetOpenNotificationsQuery() { }

    public GetOpenNotificationsQuery(Guid subjectId)
    {
      SubjectId = subjectId;
    }

    public Guid SubjectId { get; set; }
  }
}
