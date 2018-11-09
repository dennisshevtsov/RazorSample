using Microsoft.AspNetCore.Mvc;
using System;

namespace RazorSample.Web.Queries
{
  public sealed class SelectClientOwnerQuery
  {
    public SelectClientOwnerQuery() {}

    public SelectClientOwnerQuery(Guid clientOwnerId)
    {
      ClientOwnerId = clientOwnerId;
    }

    [FromQuery]
    public Guid ClientOwnerId { get; set; }
  }
}
