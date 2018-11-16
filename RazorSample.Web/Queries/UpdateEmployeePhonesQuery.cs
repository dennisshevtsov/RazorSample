using System;

namespace RazorSample.Web.Queries
{
  public sealed class UpdateEmployeePhonesQuery
  {
    public UpdateEmployeePhonesQuery() { }

    public UpdateEmployeePhonesQuery(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    public Guid EmployeeId { get; set; }
  }
}
