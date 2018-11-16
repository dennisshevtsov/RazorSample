using System;

namespace RazorSample.Web.Queries
{
  public sealed class UpdateEmployeeEmailsQuery
  {
    public UpdateEmployeeEmailsQuery() { }

    public UpdateEmployeeEmailsQuery(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    public Guid EmployeeId { get; set; }
  }
}
