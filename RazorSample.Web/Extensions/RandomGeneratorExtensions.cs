using RazorSample.Web.Commands;
using RazorSample.Web.Services;
using System;

namespace RazorSample.Web.Extensions
{
  public static class RandomGeneratorExtensions
  {
    public static string RandomEmail(this IRandomGenerator source, string firstName, string lastName)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      return $"{firstName}.{lastName}_{source.RadomToken()}@test.test";
    }

    public static string RandomEmployeeNo(this IRandomGenerator source, string lastName)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      return $"{lastName}{source.RadomToken()}";
    }

    public static CreateEmployeeCommand RandomEmployee(this IRandomGenerator source)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      var command = new CreateEmployeeCommand
      {
        FirstName = source.RandomFirstName(),
        LastName = source.RandomLastName(),
      };

      command.Email = source.RandomEmail(command.FirstName, command.LastName);
      command.EmployeeNo = source.RandomEmployeeNo(command.LastName);

      return command;
    }
  }
}
