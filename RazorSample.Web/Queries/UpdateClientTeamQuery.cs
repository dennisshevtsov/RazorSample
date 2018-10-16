using System;

namespace RazorSample.Web.Queries
{
  public sealed class UpdateClientTeamQuery : ISearchClientsQuery, IUpdateClientQuery
  {
    public Guid ClientId { get; set; }

    public string EmployeeNo { get; set; }

    public int PageNo { get; set; }
  }
}
