using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RazorSample.Data.Entities;
using RazorSample.Web.Extensions;
using RazorSample.Web.Queries;
using RazorSample.Web.Services;

namespace RazorSample.Web.Controllers
{
  public sealed class AccountController : Controller
  {
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAccountService _accountService;

    public AccountController(IPasswordHasher passwordHasher, IAccountService accountService)
    {
      _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
      _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginQuery query, string returnUrl)
    {
      QueryExecutionResult<AccountEntity> queryExecutionResult;

      if (ModelState.IsValid &&
          (queryExecutionResult = await _accountService.HandleAsync(query)) != null &&
          _passwordHasher.VerifyPassword(query.Password, queryExecutionResult.Result.DecodedPasswordHash))
      {
        var claims = queryExecutionResult.Result.GetClaims();
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        if (string.IsNullOrWhiteSpace(returnUrl))
        {
          return Redirect(Url.AppUri(nameof(EmployeeController.Index), nameof(EmployeeController)));
        }

        return Redirect(returnUrl);
      }

      return View("LoginView");
    }
  }
}
