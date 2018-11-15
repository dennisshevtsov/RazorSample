using System.ComponentModel.DataAnnotations;

namespace RazorSample.Web.Commands
{
  public class CreateEmployeeCommand : EmployeeFormCommandBase
  {
    [Required]
    public string Email { get; set; }
  }
}
