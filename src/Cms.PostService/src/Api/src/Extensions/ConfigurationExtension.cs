using Cms.Shared.Setups;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.PostService.Api.Extensions;

public static class ConfigurationExtension
{
    public static void SetupApiConfiguration(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddMessagingBrokerConfiguration(configuration);
    }
}
