using System;
using System.Collections.Generic;

namespace RazorSample.Web.Commands
{
  public sealed class UpdateEmployeeEmailsCommand
  {
    public Guid EmployeeId { get; set; }

    public IEnumerable<string> Emails { get; set; }
  }
}
