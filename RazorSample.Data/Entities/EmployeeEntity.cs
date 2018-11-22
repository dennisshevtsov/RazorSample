using System;

namespace RazorSample.Data.Entities
{
  public sealed class EmployeeEntity : SubjectEntityBase
  {
    public Guid EmployeeId { get { return _subjectId; } set { _subjectId = value; } }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{LastName}, {FirstName} <{EmployeeNo}>";

    public string EmployeeNo { get; set; }
  }
}
