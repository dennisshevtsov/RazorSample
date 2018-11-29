using Microsoft.EntityFrameworkCore;
using RazorSample.Data.Entities;
using System;
using System.Linq;

namespace RazorSample.Data.Specifications
{
  public sealed class OpenNotificationsOfSubjectSpecification : Specification<NotificationEntity>
  {
    public OpenNotificationsOfSubjectSpecification(Guid subjectId)
    {
      SubjectId = subjectId;
    }

    public Guid SubjectId { get; }

    protected internal override IQueryable<NotificationEntity> Apply(IQueryable<NotificationEntity> query)
    {
      return query.Where(notification => notification.SubjectId == SubjectId && notification.Closed == null)
                  .AsNoTracking();
    }
  }
}
