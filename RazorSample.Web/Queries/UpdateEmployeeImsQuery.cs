using System;

namespace RazorSample.Web.Queries
{
  public sealed class UpdateEmployeeImsQuery
  {
    public UpdateEmployeeImsQuery() { }

    public UpdateEmployeeImsQuery(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    public Guid EmployeeId { get; set; }
  }
}
