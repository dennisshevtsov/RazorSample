using System.ComponentModel.DataAnnotations;

namespace RazorSample.Web.Commands
{
  public abstract class EmployeeFormCommandBase
  {
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string EmployeeNo { get; set; }
  }
}
