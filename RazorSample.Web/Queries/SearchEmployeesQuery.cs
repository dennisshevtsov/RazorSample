using System;
using Microsoft.AspNetCore.Mvc;

namespace RazorSample.Web.Queries
{
  public sealed class SearchEmployeesQuery
  {
    public SearchEmployeesQuery() { }

    public SearchEmployeesQuery(string employeeNo)
    {
      EmployeeNo = employeeNo ?? throw new ArgumentNullException(nameof(employeeNo));
    }

    [FromQuery]
    public string EmployeeNo { get; set; }

    [FromQuery]
    public int PageNo { get; set; }

    internal int PageSize => 10;
  }
}
