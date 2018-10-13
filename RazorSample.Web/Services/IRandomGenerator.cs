namespace RazorSample.Web.Services
{
  public interface IRandomGenerator
  {
    string RadomToken();

    string RandomFirstName();
    string RandomLastName();
  }
}
