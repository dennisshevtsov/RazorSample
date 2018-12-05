using System;
using System.ComponentModel.DataAnnotations;

namespace RazorSample.Web.Commands
{
  public sealed class AddEmployeeAddressCommand
  {
    public Guid EmployeeId { get; set; }
    public Guid SubjectId => EmployeeId;

    [Required]
    public string Address { get; set; }

    public string Zip { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public string Description { get; set; }
  }
}
