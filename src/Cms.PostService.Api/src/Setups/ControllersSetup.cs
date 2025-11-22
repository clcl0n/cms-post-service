using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.PostService.Api.Setups;

public static class ControllersSetup
{
    public static void SetupControllers(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(
                // show enum value in swagger.
                new JsonStringEnumConverter()
            );
        });
    }
}
