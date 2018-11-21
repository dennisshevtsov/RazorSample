using System;

namespace RazorSample.Web.Commands
{
  public sealed class AddEmployeePhoneCommand
  {
    public Guid EmployeeId { get; set; }
    public Guid SubjectId => EmployeeId;

    public string Phone { get; set; }
    public string Description { get; set; }
  }
}
