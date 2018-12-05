using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace RazorSample.Web.Services
{
  public sealed class PasswordHasher : IPasswordHasher
  {
    internal const int SaltBits = 128;
    internal const int SaltArrayLength = SaltBits / 8;

    internal const int SubkeyBits = 256;
    internal const int SubkeyArrayLength = SubkeyBits / 8;

    internal const int Interactions = 10000;

    public byte[] HashPassword(string password)
    {
      var salt = new byte[SaltArrayLength];

      using (var rng = RandomNumberGenerator.Create())
      {
        rng.GetBytes(salt);
      }

      var subkey = Subkey(password, salt);

      var passwordHash = new byte[SaltArrayLength + SubkeyArrayLength];

      Buffer.BlockCopy(salt, 0, passwordHash, 0, SaltArrayLength);
      Buffer.BlockCopy(subkey, 0, passwordHash, SaltArrayLength, SubkeyArrayLength);

      return passwordHash;
    }

    public bool VerifyPassword(string password, byte[] passwordHash)
    {
      var salt = new byte[SaltArrayLength];
      Buffer.BlockCopy(passwordHash, 0, salt, 0, SaltArrayLength);

      var subkey = new byte[SubkeyArrayLength];
      Buffer.BlockCopy(passwordHash, SaltArrayLength, subkey, 0, SubkeyArrayLength);

      var hashedSubkey = Subkey(password, salt);

      for (var i = 0; i < subkey.Length; ++i)
      {
        if (hashedSubkey[i] != subkey[i])
        {
          return false;
        }
      }

      return true;
    }

    internal byte[] Subkey(string password, byte[] salt)
    {
      var subkey = KeyDerivation.Pbkdf2(
        password, salt, KeyDerivationPrf.HMACSHA1, Interactions, SubkeyArrayLength);

      return subkey;
    }
  }
}
