using System;

namespace RazorSample.Web.Services
{
  public sealed class RandomGenerator : IRandomGenerator
  {
    private static readonly string[] FirstNames = new[] { "John", "Joan", "Alexander", "Alexandra", "Daniel", "Daniela",
                                                          "Robert", "Roberta", "Dennis", "Denise", "Paul", "Paula",
                                                          "Harry", "Marry", "George", "Zoe", "James", "Ann", };
    private static readonly string[] LastNames = new[] { "Johnson", "Johns", "Alexanderson", "Alexanders", "Danielson", "Daniels",
                                                         "Robertson", "Roberts", "Harryson", "Fitzpatric", "Jameson", };
    private static readonly string[] CompanyNamePostfixes = new[] { "Ltd.", "Inc.", "Lab.", };

    private readonly Random _random;

    public RandomGenerator()
    {
      _random = new Random();
    }

    public string RandomToken()
    {
      return Guid.NewGuid()
                 .ToString()
                 .Replace("-", "")
                 .Substring(0, 5)
                 .ToUpper();
    }

    public string RandomFirstName()
    {
      return RandomArrayElement(FirstNames);
    }

    public string RandomLastName()
    {
      return RandomArrayElement(LastNames);
    }

    public string RandomCompanyName()
    {
      return $"{RandomToken()} {RandomArrayElement(CompanyNamePostfixes)}";
    }

    private string RandomArrayElement(string[] array)
    {
      return array[_random.Next(0, array.Length - 1)];
    }
  }
}
