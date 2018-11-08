using Microsoft.EntityFrameworkCore;
using RazorSample.Data.Entities;
using System;
using System.Linq;

namespace RazorSample.Data.Specifications
{
  public sealed class EmployeesWithNameLikeSpecification : Specification<EmployeeEntity>
  {
    public EmployeesWithNameLikeSpecification(string namePart)
    {
      NamePart = namePart;
    }

    public string NamePart { get; }

    protected internal override IQueryable<EmployeeEntity> Apply(IQueryable<EmployeeEntity> query)
    {
      if (string.IsNullOrWhiteSpace(NamePart) == false)
      {
        var namePart = NamePart.Trim();
        var nameParts = namePart.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (nameParts.Length == 1)
        {
          query = query.Where(employee => employee.FirstName.Contains(nameParts[0]) || employee.LastName.Contains(nameParts[0]));
        }
        else
        {
          query = query.Where(employee => (employee.FirstName.Contains(nameParts[0]) &&
                                           employee.LastName.Contains(nameParts[1])) ||
                                          (employee.FirstName.Contains(nameParts[1]) &&
                                           employee.LastName.Contains(nameParts[0])));
        }
      }

      return query.OrderBy(employee => employee.EmployeeId)
                  .OrderBy(employee => employee.Created)
                  .AsNoTracking();
    }
  }
}
