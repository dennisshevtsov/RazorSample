using System;
using System.Collections.Generic;
using System.Text;

namespace RazorSample.Random
{
  public interface IRandomGenerator
  {
    string RandomToken();

    string RandomFirstName();
    string RandomLastName();

    string RandomCompanyName();
    string RandomBusinessEntityType();
  }
}
