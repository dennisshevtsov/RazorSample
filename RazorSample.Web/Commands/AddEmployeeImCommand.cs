using System;

namespace RazorSample.Web.Commands
{
  public sealed class AddEmployeeImCommand
  {
    public Guid EmployeeId { get; set; }
    public Guid SubjectId => EmployeeId;

    public string Im { get; set; }
    public string Description { get; set; }
  }
}
