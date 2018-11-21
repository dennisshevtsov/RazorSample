using System;

namespace RazorSample.Web.Queries
{
  public sealed class RemoveEmployeeEmailQuery
  {
    public RemoveEmployeeEmailQuery() { }

    public RemoveEmployeeEmailQuery(Guid employeeId, Guid emailId)
    {
      EmployeeId = employeeId;
      EmailId = emailId;
    }

    public Guid EmployeeId { get; set; }
    public Guid EmailId { get; set; }
  }
}
