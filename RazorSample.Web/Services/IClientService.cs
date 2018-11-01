using RazorSample.Data;
using RazorSample.Data.Entities;
using RazorSample.Web.Commands;
using RazorSample.Web.Queries;

namespace RazorSample.Web.Services
{
  public interface IClientService : IQueryHandler<SearchClientQuery, Page<ClientEntity>>,
                                    IQueryHandler<CreateClientQuery, CreateClientCommand>,
                                    IQueryHandler<UpdateClientQuery, ClientEntity>,
                                    ICommandHandler<CreateClientCommand>,
                                    ICommandHandler<UpdateClientCommand>
  { }
}
