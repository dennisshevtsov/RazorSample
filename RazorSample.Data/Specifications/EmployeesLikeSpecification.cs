using RazorSample.Data.Entities;
using System.Linq;

namespace RazorSample.Data.Specifications
{
  public sealed class EmployeesLikeSpecification : Specification<EmployeeEntity>
  {
    public EmployeesLikeSpecification(string employeeNo)
    {
      EmployeeNo = employeeNo;
    }

    public string EmployeeNo { get; }

    protected internal override IQueryable<EmployeeEntity> Apply(IQueryable<EmployeeEntity> query)
    {
      if (string.IsNullOrWhiteSpace(EmployeeNo) == false)
      {
        var employeeNo = EmployeeNo.Trim();

        query = query.Where(employee => employee.EmployeeNo.Contains(employeeNo));
      }

      return query.OrderBy(employee => employee.EmployeeId)
                  .OrderBy(employee => employee.Created);
    }
  }
}
