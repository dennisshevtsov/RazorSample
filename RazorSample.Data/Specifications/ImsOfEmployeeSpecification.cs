using RazorSample.Data.Entities;
using System;
using System.Linq;

namespace RazorSample.Data.Specifications
{
  public sealed class ImsOfEmployeeSpecification : Specification<ImEntity>
  {
    public ImsOfEmployeeSpecification(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    public Guid EmployeeId { get; }

    protected internal override IQueryable<ImEntity> Apply(IQueryable<ImEntity> query)
    {
      return query.Where(email => email.SubjectId == EmployeeId);
    }
  }
}
