using System;
using System.Linq;

namespace RazorSample.Data
{
    public sealed class AllEmployeesSpecification : Specification<EmployeeEntity>
    {
        protected internal override IQueryable<EmployeeEntity> Apply(IQueryable<EmployeeEntity> query)
        {
            return query.OrderBy(employee => employee.EmployeeId)
                        .OrderBy(employee => employee.Created);
        }
    }
}
