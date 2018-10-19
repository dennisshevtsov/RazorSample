using System;

namespace RazorSample.Web.Queries
{
  public interface IUpdateClientQuery
  {
    Guid ClientId { get; }
  }

  public sealed class UpdateClientQuery : IUpdateClientQuery
  {
    public UpdateClientQuery() { }

    public UpdateClientQuery(Guid clientId)
    {
      ClientId = clientId;
    }

    public Guid ClientId { get; set; }
  }
}
