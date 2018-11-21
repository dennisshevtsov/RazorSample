using System;

namespace RazorSample.Web.Queries
{
  public sealed class SearchEmployeePhoneQuery
  {
    public SearchEmployeePhoneQuery() { }

    public SearchEmployeePhoneQuery(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    public Guid EmployeeId { get; set; }
  }
}
