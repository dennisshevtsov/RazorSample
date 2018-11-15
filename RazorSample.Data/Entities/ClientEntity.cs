using System;
using System.Collections.Generic;

namespace RazorSample.Data.Entities
{
  public sealed class ClientEntity : SubjectEntityBase
  {
    public Guid ClientId { get { return _subjectId; } set { _subjectId = value; } }

    public string ClientNo { get; set; }
    public string OrganizationNo { get; set; }

    public string Name { get; set; }

    public Guid ClientOwnerId { get; set; }
    public EmployeeEntity ClientOwner { get; set; }

    public ICollection<ContactEntity> Relations { get; set; }
  }
}
