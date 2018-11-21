using System;

namespace RazorSample.Web.Queries
{
  public sealed class SearchEmployeeEmailQuery
  {
    public SearchEmployeeEmailQuery() { }

    public SearchEmployeeEmailQuery(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    public Guid EmployeeId { get; set; }
  }
}
