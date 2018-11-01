using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorSample.Web.Queries
{
  public sealed class CreateClientQuery
  {
    public CreateClientQuery() { }

    public CreateClientQuery(Guid? clientOwnerId)
    {
      ClientOwnerId = clientOwnerId;
    }

    public Guid? ClientOwnerId { get; set; }
  }
}
