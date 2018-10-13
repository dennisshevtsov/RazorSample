using RazorSample.Data.Entities;
using RazorSample.Web.Queries;
using System;

namespace RazorSample.Web.ViewModels
{
  public class EmployeeListVm : ListVmBase<SearchEmployeesQuery, EmployeeListItemVm> { }

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
