using Microsoft.AspNetCore.Mvc;
using RazorSample.Data.Entities;
using System;

namespace RazorSample.Web.Commands
{
  public sealed class UpdateEmployeeCommand : EmployeeFormCommandBase
  {
    public UpdateEmployeeCommand() { }

    public UpdateEmployeeCommand(EmployeeEntity employeeEntity)
    {
      EmployeeId = employeeEntity.EmployeeId;
      FirstName = employeeEntity.FirstName;
      LastName = employeeEntity.LastName;
      EmployeeNo = employeeEntity.EmployeeNo;
      Email = employeeEntity.Email;
    }

    [FromQuery]
    public Guid EmployeeId { get; set; }

    public string Phone { get; set; }
    public string Address { get; set; }
  }
}
