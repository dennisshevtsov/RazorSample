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

    public SearchEmployeeAddressQuery(Guid employeeId, Guid messageId) : this(employeeId)
    {
      MessageId = messageId;
    }

    public Guid EmployeeId { get; set; }
    public Guid? MessageId { get; set; }
  }
}
