using Microsoft.EntityFrameworkCore;
using RazorSample.Data.Entities;
using System;
using System.Linq;

namespace RazorSample.Data.Specifications
{
  public sealed class ClientLikeSpecification : Specification<ClientEntity>
  {
    public ClientLikeSpecification(string clientNo)
    {
      ClientNo = clientNo;
    }

    public string ClientNo { get; }

    protected internal override IQueryable<ClientEntity> Apply(IQueryable<ClientEntity> query)
    {
      if (string.IsNullOrWhiteSpace(ClientNo) == false)
      {
        query = query.Where(client => client.ClientNo.Contains(ClientNo, StringComparison.InvariantCultureIgnoreCase));
      }

      return query.OrderBy(client => client.ClientId)
                  .OrderBy(client => client.Name)
                  .AsNoTracking();
    }
  }
}
