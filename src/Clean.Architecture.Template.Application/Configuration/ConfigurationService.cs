using Clean.Architecture.Template.Core.Services;
using Microsoft.Extensions.Configuration;

namespace Clean.Architecture.Template.Application.Configuration
{
    public class ConfigurationService(IConfiguration configuration) : IConfigurationService
    {
        private readonly IConfiguration _configuration = configuration;

        public string ConnectionString() => GetConnectionString("PGSQL_CONNECTION_STRING")!;

        private string? GetConnectionString(string configName)
        {
            return _configuration.GetValue<string>($"ConnectionStrings:{configName}")
                ?? Environment.GetEnvironmentVariable(configName)
                ?? Environment.GetEnvironmentVariable($"SQLCONNSTR_{configName}")
                ?? Environment.GetEnvironmentVariable($"ConnectionStrings:{configName}");
        }
    }
}
