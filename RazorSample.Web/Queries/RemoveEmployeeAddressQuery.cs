using Microsoft.AspNetCore.Mvc;
using System;

namespace RazorSample.Web.Queries
{
  public sealed class RemoveEmployeeAddressQuery
  {
    public RemoveEmployeeAddressQuery() { }

    public RemoveEmployeeAddressQuery(Guid employeeId, Guid addressId)
    {
      EmployeeId = employeeId;
      AddressId = addressId;
    }

    [FromQuery]
    public Guid EmployeeId { get; set; }

    [FromQuery]
    public Guid AddressId { get; set; }
  }
}
