using RazorSample.Data.Entities;
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

      return $"{firstName}.{lastName}_{source.RandomToken()}@test.test";
    }

    public static string RandomNo(this IRandomGenerator source, string name)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      return $"{name}{source.RandomToken()}";
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
      command.EmployeeNo = source.RandomNo(command.LastName);

      return command;
    }

    public static ClientEntity RandomClient(this IRandomGenerator source)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      var command = new ClientEntity
      {
        Name = source.RandomCompanyName(),
      };

      command.ClientNo = source.RandomNo(command.Name);
      command.OrganizationNo = source.RandomNo(command.Name);

      return command;
    }
  }
}
