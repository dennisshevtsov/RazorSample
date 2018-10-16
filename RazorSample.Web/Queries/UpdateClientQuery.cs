using System;

namespace RazorSample.Web.Queries
{
  public interface IUpdateClientQuery
  {
    Guid ClientId { get; }
  }

  public sealed class UpdateClientQuery : IUpdateClientQuery
  {
    public Guid ClientId { get; set; }
  }
}
