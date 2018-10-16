using Microsoft.AspNetCore.Mvc;

namespace RazorSample.Web.Controllers
{
  public sealed class ClientController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
