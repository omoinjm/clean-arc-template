using Clean.Architecture.Template.Core.Services;

namespace Clean.Architecture.Template.Application.Configuration
{
    public class AzureConfigurationService : IConfigurationService
    {
        public string ConnectionString()
        {
            return Environment.GetEnvironmentVariable("SQLCONNSTR_POSTGRES_CONNECTION_STRING")
                ?? Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING")!;
        }
    }
}