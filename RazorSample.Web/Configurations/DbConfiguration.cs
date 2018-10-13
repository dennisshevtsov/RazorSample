using Microsoft.Extensions.Configuration;
using System;

namespace RazorSample.Web.Configurations
{
    internal sealed class DbConfiguration
    {
        private readonly IConfiguration _configuration;

        public DbConfiguration(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        internal string ConnectionString => _configuration.GetConnectionString("RazorSample");
    }
}
