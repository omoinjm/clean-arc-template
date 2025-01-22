using Clean.Architecture.Template.Core.Services;
using Microsoft.Extensions.Configuration;

namespace Clean.Architecture.Template.Application.Configuration
{
    public class LocalConfigurationService(IConfiguration configuration) : IConfigurationService
    {
        private readonly IConfiguration _configuration = configuration;

        public string ConnectionString()
        {
            return _configuration.GetValue<string>("ConnectionString:POSTGRES_CONNECTION_STRING")
                ?? Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING")!;
        }
    }
}