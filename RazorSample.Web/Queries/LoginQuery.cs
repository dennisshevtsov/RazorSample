using System.ComponentModel.DataAnnotations;

namespace RazorSample.Web.Queries
{
  public sealed class LoginQuery
  {
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
  }
}
