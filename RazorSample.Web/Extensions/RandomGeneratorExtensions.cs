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

    public static string RandomClientNo(this IRandomGenerator source, string name)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      if (string.IsNullOrWhiteSpace(name))
      {
        return source.RandomToken();
      }

      return name.Replace(" ", "");
    }

    public static string RandomEmployeeNo(this IRandomGenerator source, string firstName, string lastName)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      return $"{firstName[0]}{lastName}{source.RandomToken()}".Replace(" ", "").ToLowerInvariant();
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
      command.EmployeeNo = source.RandomEmployeeNo(command.FirstName, command.LastName);

      return command;
    }

    public static ClientEntity RandomClient(this IRandomGenerator source)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      var companyName = source.RandomCompanyName();
      var command = new ClientEntity
      {
        Name = $"{companyName} {source.RandomBusinessEntityType()}",
        ClientNo = source.RandomClientNo(companyName),
        OrganizationNo = source.RandomClientNo(companyName),
      };

      return command;
    }
  }
}
