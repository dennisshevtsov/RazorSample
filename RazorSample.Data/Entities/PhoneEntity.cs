using System;

namespace RazorSample.Data.Entities
{
  public class PhoneEntity
  {
    public Guid PhoneId { get; set; }
    public Guid SubjectId { get; set; }

    public string Phone { get; set; }
    public string Description { get; set; }
  }
}
