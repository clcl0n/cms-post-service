using Cms.Shared.Setups;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wolverine;
using Wolverine.RabbitMQ;

namespace Cms.PostService.Api.Setups;

public static class WolverineSetup
{
    public static void SetupWolverine(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddWolverine(options =>
        {
            options.UseRabbitMq(configuration);
        });
    }
}
