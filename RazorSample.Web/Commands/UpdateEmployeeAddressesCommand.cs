using System;
using System.Collections.Generic;

namespace RazorSample.Web.Commands
{
  public sealed class UpdateEmployeeAddressesCommand
  {
    public Guid EmployeeId { get; set; }

    public IEnumerable<string> Addresses { get; set; }
  }
}
