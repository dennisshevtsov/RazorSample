using RazorSample.Data;
using RazorSample.Data.Entities;
using RazorSample.Web.Commands;
using RazorSample.Web.Queries;
using System.Collections.Generic;

namespace RazorSample.Web.Services
{
  public interface IClientService : IQueryHandler<SearchClientQuery, Page<ClientEntity>>,
                                    IQueryHandler<CreateClientQuery, ClientEntity>,
                                    IQueryHandler<UpdateClientQuery, ClientEntity>,
                                    IQueryHandler<SearchClientOwnerQuery, IEnumerable<EmployeeEntity>>,
                                    ICommandHandler<CreateClientCommand>,
                                    ICommandHandler<UpdateClientCommand>
  { }
}
