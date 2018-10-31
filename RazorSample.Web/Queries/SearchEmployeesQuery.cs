using System;
using Microsoft.AspNetCore.Mvc;

namespace RazorSample.Web.Queries
{
  public sealed class SearchEmployeesQuery
  {
    public SearchEmployeesQuery() { }

    public SearchEmployeesQuery(string employeeNo)
    {
      EmployeeNo = employeeNo;
    }

    public SearchEmployeesQuery(string employeeNo, int pageNo) : this(employeeNo)
    {
      PageNo = pageNo;
    }

    [FromQuery]
    public string EmployeeNo { get; set; }

    [FromQuery]
    public int PageNo { get; set; }

    internal int PageSize => 10;
  }
}
