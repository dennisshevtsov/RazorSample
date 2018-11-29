using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorSample.Web.Queries
{
  public sealed class CloseNotificationQuery
  {
    public CloseNotificationQuery() { }

    public CloseNotificationQuery(Guid notificationId)
    {
      NotificationId = notificationId;
    }

    public Guid NotificationId { get; set; }
  }
}
