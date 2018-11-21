using System;

namespace RazorSample.Web.Queries
{
  public sealed class RemoveEmployeeImQuery
  {
    public RemoveEmployeeImQuery() { }

    public RemoveEmployeeImQuery(Guid employeeId, Guid imId)
    {
      EmployeeId = employeeId;
      ImId = imId;
    }

    public Guid EmployeeId { get; set; }
    public Guid ImId { get; set; }
  }
}
