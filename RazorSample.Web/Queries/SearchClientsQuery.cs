namespace RazorSample.Web.Queries
{
  public interface ISearchClientsQuery
  {
    string EmployeeNo { get; }

    int PageNo { get; }
  }

  public class SearchClientsQuery : ISearchClientsQuery
  {
    public string EmployeeNo { get; set; }

    public int PageNo { get; set; }
  }
}
