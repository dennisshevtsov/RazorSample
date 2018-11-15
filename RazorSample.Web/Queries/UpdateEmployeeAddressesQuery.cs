using System;

namespace RazorSample.Web.Queries
{
  public sealed class UpdateEmployeeAddressesQuery
  {
    public UpdateEmployeeAddressesQuery() { }

    public UpdateEmployeeAddressesQuery(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    public Guid EmployeeId { get; set; }
  }
}
