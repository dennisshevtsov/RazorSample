using Microsoft.AspNetCore.Mvc;
using System;

namespace RazorSample.Web.Queries
{
  public interface IUpdateEmployeeQuery
  {
    Guid EmployeeId { get; }
  }

  public sealed class GetEmployeeQuery : IUpdateEmployeeQuery
  {
    [FromQuery]
    public Guid EmployeeId { get; set; }
  }
}
