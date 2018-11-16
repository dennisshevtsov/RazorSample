using Microsoft.AspNetCore.Mvc;
using System;

namespace RazorSample.Web.Queries
{
  public sealed class UpdateEmployeeGeneralInfoQuery
  {
    public UpdateEmployeeGeneralInfoQuery() { }

    public UpdateEmployeeGeneralInfoQuery(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    [FromQuery]
    public Guid EmployeeId { get; set; }
  }
}
