using Microsoft.AspNetCore.Mvc;
using System;

namespace RazorSample.Web.Queries
{
  public sealed class RemoveAddressQuery
  {
    [FromQuery]
    public Guid EmployeeId { get; set; }
  }
}
