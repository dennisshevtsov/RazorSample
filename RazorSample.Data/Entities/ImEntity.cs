using System;

namespace RazorSample.Data.Entities
{
  public sealed class ImEntity
  {
    public Guid SubjectId { get; set; }

    public string Im { get; set; }
    public string Description { get; set; }
  }
}
