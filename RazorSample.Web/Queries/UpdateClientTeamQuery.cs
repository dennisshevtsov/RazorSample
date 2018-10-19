using System;

namespace RazorSample.Web.Queries
{
    public sealed class UpdateClientTeamQuery : ISearchClientsQuery, IUpdateClientQuery
    {
        public Guid ClientId { get; set; }

        public string ClientNo { get; set; }

        public int PageNo { get; set; }

        public Guid ClientTeamToken { get; set; }
    }
}
