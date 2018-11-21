using RazorSample.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorSample.Data.Specifications
{
  public sealed class EmailForIdSpecification : Specification<EmailEntity>
  {
    public EmailForIdSpecification(Guid emailId)
    {
      EmailId = emailId;
    }

    public Guid EmailId { get; set; }

    protected internal override IQueryable<EmailEntity> Apply(IQueryable<EmailEntity> query)
    {
      return query.Where(entity => entity.EmailId == EmailId);
    }
  }
}
