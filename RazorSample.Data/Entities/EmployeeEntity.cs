using System;

namespace RazorSample.Data.Entities
{
  public sealed class EmployeeEntity : SubjectEntityBase
  {
    public Guid EmployeeId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{LastName}, {FirstName}";

    public string EmployeeNo { get; set; }
  }
}
