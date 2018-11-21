using RazorSample.Data.Entities;
using System;
using System.Linq;

namespace RazorSample.Data.Specifications
{
  public sealed class PhoneForIdSpecification : Specification<PhoneEntity>
  {
    public PhoneForIdSpecification(Guid phoneId)
    {
      PhoneId = phoneId;
    }

    public Guid PhoneId { get;}

    protected internal override IQueryable<PhoneEntity> Apply(IQueryable<PhoneEntity> query)
    {
      return query.Where(entity => entity.PhoneId == PhoneId);
    }
  }
}
