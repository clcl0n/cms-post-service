using Cms.PostService.Api.Setups;
using Cms.PostService.Application;
using Cms.PostService.Infrastructure;
using Cms.Shared.Setups;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;

namespace Cms.PostService.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication
            .CreateBuilder(args)
            .SetupBuilder();

        using var app = builder
            .Build()
            .SetupApplication();

        app.RunWithGraphQLCommands(args);
    }

    private static WebApplicationBuilder SetupBuilder(this WebApplicationBuilder builder)
    {
        builder.Logging.SetupOpenTelemetry();
        builder.Services.SetupOpenTelemetry();

        var healthChecksBuilder = builder.Services.AddHealthChecks();

        healthChecksBuilder.SetupHealthCheck(builder.Configuration);

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(healthChecksBuilder, builder.Configuration);

        builder.Services.SetupControllers();
        builder.SetupGraphQL();
        builder.Services.SetupProblemDetails(builder.Environment);
        builder.Services.SetupApiConfiguration(builder.Configuration);
        builder.Services.SetupWolverine(builder.Configuration);

        builder.Services.AddOpenApi();

        return builder;
    }

    private static WebApplication SetupApplication(this WebApplication app)
    {
        app.MapOpenApi();

        app.UseExceptionHandler();
        app.UseStatusCodePages();

        app.UseHealthCheck();

        app.MapControllers();

        app.MapScalarApiReference();

        app.MapGraphQL();

        return app;
    }
}
