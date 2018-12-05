using System;

namespace RazorSample.Data.Entities
{
  public sealed class AccountEntity
  {
    public AccountEntity() { }

    public AccountEntity(string email, byte[] passwordHash)
    {
      Email = email ?? throw new ArgumentNullException(nameof(email));
      PasswordHash = Convert.ToBase64String(passwordHash) ?? throw new ArgumentNullException(nameof(passwordHash));
    }

    public Guid AccountId { get; set; }

    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public byte[] DecodedPasswordHash => Convert.FromBase64String(PasswordHash);

    public bool IsActive { get; set; }
    public bool IsVerified { get; set; }
  }
}
