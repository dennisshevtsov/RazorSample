using System;
using Microsoft.AspNetCore.Mvc;

namespace RazorSample.Web.Queries
{
  public sealed class SearchEmployeeQuery
  {
    public SearchEmployeeQuery() { }

    public SearchEmployeeQuery(string employeeNo)
    {
      EmployeeNo = employeeNo;
    }

    public SearchEmployeeQuery(string employeeNo, int pageNo) : this(employeeNo)
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
