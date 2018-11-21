using Microsoft.AspNetCore.Mvc;
using System;

namespace RazorSample.Web.Commands
{
  public sealed class AddEmployeeEmailCommand
  {
    [FromQuery]
    public Guid EmployeeId { get; set; }
    public Guid SubjectId => EmployeeId;

    public string Email { get; set; }
    public string Description { get; set; }
  }
}
