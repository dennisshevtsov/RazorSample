using Microsoft.AspNetCore.Mvc;
using System;

namespace RazorSample.Web.Commands
{
  public class RemoveEmployeeAddressCommand
  {
    [FromQuery]
    public Guid AddressId { get; set; }
  }
}
