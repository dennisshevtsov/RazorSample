using System;

namespace RazorSample.Web.Queries
{
  public class RemoveEmployeePhoneQuery
  {
    public RemoveEmployeePhoneQuery() { }

    public RemoveEmployeePhoneQuery(Guid employeeId, Guid phoneId)
    {
      EmployeeId = employeeId;
      PhoneId = phoneId;
    }

    public Guid EmployeeId { get; set; }
    public Guid PhoneId { get; set; }
  }
}
