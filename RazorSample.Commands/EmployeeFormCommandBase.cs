using System.ComponentModel.DataAnnotations;

namespace RazorSample.Commands
{
  public abstract class EmployeeFormCommandBase
  {
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
