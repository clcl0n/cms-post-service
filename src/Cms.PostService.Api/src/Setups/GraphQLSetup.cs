using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cms.PostService.Api.Setups;

public static class GraphQLSetup
{
    public static void SetupGraphQL(this WebApplicationBuilder builder)
    {
        builder
            .AddGraphQL()
            .AddTypes();
    }
}
