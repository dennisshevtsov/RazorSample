using Microsoft.AspNetCore.Mvc;
using System;

namespace RazorSample.Web.Queries
{
  public sealed class UpdateEmployeeQuery
  {
    public UpdateEmployeeQuery() { }

    public UpdateEmployeeQuery(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    [FromQuery]
    public Guid EmployeeId { get; set; }
  }
}
