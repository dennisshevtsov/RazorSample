using Microsoft.EntityFrameworkCore;
using RazorSample.Data.Entities;
using System;
using System.Linq;

namespace RazorSample.Data.Specifications
{
  public sealed class ClientWithIdSpecification : Specification<ClientEntity>
  {
    public ClientWithIdSpecification(Guid clientId)
    {
      ClientId = clientId;
    }

    public Guid ClientId { get; }

    protected internal override IQueryable<ClientEntity> Apply(IQueryable<ClientEntity> query)
    {
      return query.Where(client => client.ClientId == ClientId)
                  .Include(client => client.ClientOwner)
                  .AsNoTracking();
    }
  }
}
