using System;

namespace RazorSample.Web.Queries
{
  public sealed class SearchEmployeeImQuery
  {
    public SearchEmployeeImQuery() { }

    public SearchEmployeeImQuery(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    public Guid EmployeeId { get; set; }
  }
}
