using RazorSample.Data.Entities;
using System;
using System.Linq;

namespace RazorSample.Data.Specifications
{
  public sealed class AddressForIdSpecification : Specification<AddressEntity>
  {
    public AddressForIdSpecification(Guid addressId)
    {
      AddressId = addressId;
    }

    public Guid AddressId { get; }

    protected internal override IQueryable<AddressEntity> Apply(IQueryable<AddressEntity> query)
    {
      return query.Where(entity => entity.AddressId == AddressId);
    }
  }
}
