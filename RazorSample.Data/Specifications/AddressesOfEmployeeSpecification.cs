using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RazorSample.Data.Entities;

namespace RazorSample.Data.Specifications
{
  public sealed class AddressesOfEmployeeSpecification : Specification<AddressEntity>
  {
    public AddressesOfEmployeeSpecification(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    public Guid EmployeeId { get; }

    protected internal override IQueryable<AddressEntity> Apply(IQueryable<AddressEntity> query)
    {
      return query.Where(address => address.SubjectId == EmployeeId);
    }
  }
}
