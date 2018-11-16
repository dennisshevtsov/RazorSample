using RazorSample.Data.Entities;
using System;
using System.Linq;

namespace RazorSample.Data.Specifications
{
  public sealed class PhonesOfEmployeeSpecification : Specification<PhoneEntity>
  {
    public PhonesOfEmployeeSpecification(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    public Guid EmployeeId { get; }

    protected internal override IQueryable<PhoneEntity> Apply(IQueryable<PhoneEntity> query)
    {
      return query.Where(email => email.SubjectId == EmployeeId);
    }
  }
}
