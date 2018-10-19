namespace RazorSample.Web.Queries
{
  public interface ISearchClientsQuery
  {
    string ClientNo { get; }

    int PageNo { get; }
  }

  public class SearchClientsQuery : ISearchClientsQuery
  {
    public string ClientNo { get; set; }

    public int PageNo { get; set; }
  }
}
