using System;
using System.Collections.Generic;

namespace RazorSample.Data.Entities
{
  public abstract class SubjectEntityBase
  {
    public IEnumerable<EmailEntity> Emails { get; set; }
    public IEnumerable<PhoneEntity> Phones { get; set; }
    public IEnumerable<ImEntity> Ims { get; set; }

    public IEnumerable<AddressEntity> Addresses { get; set; }

    public DateTime Created { get; set; }

    public bool IsActive { get; set; }
  }
}
