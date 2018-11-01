using RazorSample.Data;

namespace RazorSample.Web.Queries
{
  public sealed class SearchClientQuery
  {
    public SearchClientQuery() { }

    public SearchClientQuery(string clientNo, int pageNo)
    {
      ClientNo = clientNo;
      PageNo = pageNo;
    }

    public string ClientNo { get; set; }
    public int PageNo { get; set; }

    internal int PageSize => Page.DefaultPageSize;
  }
}
