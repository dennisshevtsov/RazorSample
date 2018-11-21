using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorSample.Web.Commands
{
  public sealed class RemoveEmployeeEmailCommand
  {
    public Guid EmailId { get; set; }
  }
}
