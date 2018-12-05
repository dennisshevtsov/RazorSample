using RazorSample.Data.Entities;
using System;
using System.Linq;

namespace RazorSample.Data.Specifications
{
  public sealed class AccountWithEmailSpecification : Specification<AccountEntity>
  {
    public AccountWithEmailSpecification(string email)
    {
      Email = email ?? throw new ArgumentNullException(nameof(email));
    }

    public string Email { get; }

    protected internal override IQueryable<AccountEntity> Apply(IQueryable<AccountEntity> query)
    {
      return query.Where(account => account.Email == Email);
    }
  }
}
