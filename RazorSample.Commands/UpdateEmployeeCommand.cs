using System;

namespace RazorSample.Commands
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
  }
}
