using System;
using System.Collections.Generic;

namespace RazorSample.Web.Commands
{
  public sealed class UpdateEmployeeImsCommand
  {
    public Guid EmployeeId { get; set; }

    public IEnumerable<string> Ims { get; set; }
  }
}
