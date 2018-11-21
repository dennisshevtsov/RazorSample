using System;

namespace RazorSample.Web.Queries
{
  public sealed class AddEmployeePhoneQuery
  {
    public AddEmployeePhoneQuery() { }

    public AddEmployeePhoneQuery(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    public Guid EmployeeId { get; set; }
  }
}
