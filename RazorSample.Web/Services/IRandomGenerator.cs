namespace RazorSample.Web.Services
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
