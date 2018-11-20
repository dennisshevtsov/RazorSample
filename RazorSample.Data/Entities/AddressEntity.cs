using System;

namespace RazorSample.Data.Entities
{
  public sealed class AddressEntity
  {
    public Guid AddressId { get; set; }
    public Guid SubjectId { get; set; }

    public string Address { get; set; }
    public string Zip { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public string Description { get; set; }
  }
}
