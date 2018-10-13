using Microsoft.AspNetCore.Mvc;
using RazorSample.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace RazorSample.Web.Commands
{
  public sealed class UpdateEmployeeCommand
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

    [FromQuery(Name = "employeeid")]
    public Guid EmployeeId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string EmployeeNo { get; set; }

    [Required]
    public string Email { get; set; }
  }
}
