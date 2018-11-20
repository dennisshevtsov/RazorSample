using Microsoft.AspNetCore.Mvc;
using System;

namespace RazorSample.Web.Commands
{
  public class RemoveAddressCommand
  {
    [FromQuery]
    public Guid AddressId { get; set; }
  }
}
