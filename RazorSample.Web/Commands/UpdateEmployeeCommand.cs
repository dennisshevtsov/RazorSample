using Microsoft.AspNetCore.Mvc;
using RazorSample.Data.Entities;
using System;
using System.Collections.Generic;

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
      //Email = employeeEntity.Email;

      //Phone = employeeEntity.Phone;
      //Address = employeeEntity.Address;
    }

    [FromQuery]
    public Guid EmployeeId { get; set; }

    public IEnumerable<string> Emails { get; set; }
    public IEnumerable<string> Ims { get; set; }
    public IEnumerable<string> Phones { get; set; }
    public IEnumerable<string> Addresses { get; set; }
  }
}
