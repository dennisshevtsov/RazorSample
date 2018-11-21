using Microsoft.AspNetCore.Mvc;
using System;

namespace RazorSample.Web.Queries
{
  public sealed class AddEmployeeEmailQuery
  {
    public AddEmployeeEmailQuery() { }

    public AddEmployeeEmailQuery(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    [FromQuery]
    public Guid EmployeeId { get; set; }
  }
}
