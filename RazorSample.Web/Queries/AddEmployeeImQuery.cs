using System;

namespace RazorSample.Web.Queries
{
  public sealed class AddEmployeeImQuery
  {
    public AddEmployeeImQuery() { }

    public AddEmployeeImQuery(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    public Guid EmployeeId { get; set; }
  }
}
