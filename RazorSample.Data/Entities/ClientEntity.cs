using System;

namespace RazorSample.Data.Entities
{
  public sealed class ClientEntity
  {
    public Guid ClientId { get; set; }

    public string ClientNo { get; set; }
    public string OrganizationNo { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }

    public DateTime Created { get; set; }

    public bool IsActive { get; set; }

    public Guid ClientOwnerId { get; set; }
    public EmployeeEntity ClientOwner { get; set; }
  }
}
