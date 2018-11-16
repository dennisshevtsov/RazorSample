using Microsoft.EntityFrameworkCore;
using RazorSample.Data.Entities;
using System;
using System.Linq;

namespace RazorSample.Data.Specifications
{
  public sealed class EmailsOfEmployeeSpecification : Specification<EmailEntity>
  {
    public EmailsOfEmployeeSpecification(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    public Guid EmployeeId { get; }

    protected internal override IQueryable<EmailEntity> Apply(IQueryable<EmailEntity> query)
    {
      return query.Where(email => email.SubjectId == EmployeeId);
    }
  }
}
