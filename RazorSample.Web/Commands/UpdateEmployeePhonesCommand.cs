using System;
using System.Collections.Generic;

namespace RazorSample.Web.Commands
{
  public sealed class UpdateEmployeePhonesCommand
  {
    public Guid EmployeeId { get; set; }

    public IEnumerable<string> Phones { get; set; }
  }
}
