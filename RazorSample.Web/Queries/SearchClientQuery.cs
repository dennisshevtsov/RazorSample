using RazorSample.Data;

namespace RazorSample.Web.Queries
{
  public sealed class SearchClientQuery
  {
    public string ClientNo { get; set; }

    public int PageNo { get; set; }
    public int PageSize => Page.DefaultPageSize;
  }
}
