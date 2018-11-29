using System;

namespace RazorSample.Web.Queries
{
  public sealed class SearchEmployeeAddressQuery
  {
    public SearchEmployeeAddressQuery() { }

    public SearchEmployeeAddressQuery(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    public Guid EmployeeId { get; set; }
  }
}
