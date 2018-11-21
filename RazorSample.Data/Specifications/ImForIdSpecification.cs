using RazorSample.Data.Entities;
using System;
using System.Linq;

namespace RazorSample.Data.Specifications
{
  public sealed class ImForIdSpecification : Specification<ImEntity>
  {
    public ImForIdSpecification(Guid imId)
    {
      ImId = imId;
    }

    public Guid ImId { get;}

    protected internal override IQueryable<ImEntity> Apply(IQueryable<ImEntity> query)
    {
      return query.Where(entity => entity.ImId == ImId);
    }
  }
}
