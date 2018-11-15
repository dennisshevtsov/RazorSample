using System;

namespace RazorSample.Data.Entities
{
  public sealed class ContactEntity : SubjectEntityBase
  {
    public Guid ContactId { get { return _subjectId; } set { _subjectId = value; } }

    public string Name { get; set; }
    public string Desciption { get; set; }
  }
}
