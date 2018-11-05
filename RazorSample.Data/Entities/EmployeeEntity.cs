using System;

namespace RazorSample.Data.Entities
{
  public sealed class EmployeeEntity
  {
    public Guid EmployeeId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string EmployeeNo { get; set; }

    public string Email { get; set; }
    public string Phone { get; set; }

    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Zip { get; set; }
    public string City { get; set; }

    public DateTime Created { get; set; }

    public bool IsActive { get; set; }
  }
}
