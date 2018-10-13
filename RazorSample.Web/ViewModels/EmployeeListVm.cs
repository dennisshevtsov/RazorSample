using RazorSample.Data;
using RazorSample.Web.Queries;
using System;
using System.Linq;

namespace RazorSample.Web.ViewModels
{
    public class EmployeeListVm : ListVmBase<SearchEmployeesQuery, EmployeeListItemVm>
    {
        internal EmployeeListVm Use(Page<EmployeeEntity> employees)
        {
            Items = employees.Select(employee => new EmployeeListItemVm(employee)).ToArray();

            PageNo = employees.PageNo;
            PageSize = employees.PageSize;
            PageCount = employees.PageCount;

            return this;
        }
    }

    public sealed class EmployeeListItemVm
    {
        public EmployeeListItemVm(EmployeeEntity employeeEntity)
        {
            EmployeeId = employeeEntity.EmployeeId;
            FullName = $"{employeeEntity.LastName}, {employeeEntity.FirstName}";
            EmployeeNo = employeeEntity.EmployeeNo;
            Created = employeeEntity.Created;
        }

        public Guid EmployeeId { get; set; }

        public string FullName { get; set; }

        public string EmployeeNo { get; set; }

        public DateTime Created { get; set; }
    }
}
