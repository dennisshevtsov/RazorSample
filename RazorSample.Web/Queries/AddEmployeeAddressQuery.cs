using Microsoft.AspNetCore.Mvc;
using System;

namespace RazorSample.Web.Queries
{
  public sealed class AddEmployeeAddressQuery
  {
    public AddEmployeeAddressQuery() { }

    public AddEmployeeAddressQuery(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    [FromQuery]
    public Guid EmployeeId { get; set; }
  }
}
