using Microsoft.EntityFrameworkCore;
using RazorSample.Data.Entities;
using System;
using System.Linq;

namespace RazorSample.Data.Specifications
{
  public sealed class EmployeeWithIdSpecification : Specification<EmployeeEntity>
  {
    public EmployeeWithIdSpecification(Guid employeeId)
    {
      EmployeeId = employeeId;
    }

    public Guid EmployeeId { get; }

    protected internal override IQueryable<EmployeeEntity> Apply(IQueryable<EmployeeEntity> query)
    {
      return query.Where(employee => employee.EmployeeId == EmployeeId)
                  .Include(employee => employee.Addresses)
                  .Include(employee => employee.Emails)
                  .Include(employee => employee.Phones)
                  .Include(employee => employee.Ims)
                  .AsNoTracking();
    }
  }
}
