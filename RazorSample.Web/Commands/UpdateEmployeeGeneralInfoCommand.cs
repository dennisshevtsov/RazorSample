using Microsoft.AspNetCore.Mvc;
using RazorSample.Data.Entities;
using System;
using System.Collections.Generic;

namespace RazorSample.Web.Commands
{
  public sealed class UpdateEmployeeGeneralInfoCommand : EmployeeFormCommandBase
  {
    public UpdateEmployeeGeneralInfoCommand() { }

    public UpdateEmployeeGeneralInfoCommand(EmployeeEntity employeeEntity)
    {
      EmployeeId = employeeEntity.EmployeeId;
      FirstName = employeeEntity.FirstName;
      LastName = employeeEntity.LastName;
      EmployeeNo = employeeEntity.EmployeeNo;
    }

    [FromQuery]
    public Guid EmployeeId { get; set; }
  }
}
