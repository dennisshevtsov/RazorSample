using RazorSample.Data.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace RazorSample.Web.Extensions
{
  public static class AccountExtensions
  {
    public static IEnumerable<Claim> GetClaims(this AccountEntity source)
    {
      yield return new Claim(ClaimTypes.Email, source.Email);
    }
  }
}
