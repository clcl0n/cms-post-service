using Cms.Shared.Setups;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.PostService.Api.Setups;

public static class ConfigurationSetup
{
    public static void SetupApiConfiguration(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddMessagingBrokerConfiguration(configuration);
    }
}
