using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Cms.PostService.Api.Setups;

public static class OtelSetup
{
    public static void SetupOpenTelemetry(this IServiceCollection services)
    {
        services
            .AddOpenTelemetry()
            .SetupMetrics()
            .SetupTracing();
    }

    public static void SetupOpenTelemetry(this ILoggingBuilder logging)
    {
        logging.AddOpenTelemetry(options =>
        {
            options.IncludeFormattedMessage = true;

            options
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddOperatingSystemDetector())
                .AddOtlpExporter();
        });
    }

    private static IOpenTelemetryBuilder SetupMetrics(this IOpenTelemetryBuilder builder)
    {
        builder
            .WithMetrics(metrics =>
                metrics
                    .SetResourceBuilder(
                        ResourceBuilder.CreateDefault().AddOperatingSystemDetector()
                    )
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter()
            );

        return builder;
    }

    private static IOpenTelemetryBuilder SetupTracing(this IOpenTelemetryBuilder builder)
    {
        builder
            .WithTracing(tracing =>
                tracing
                    .SetResourceBuilder(
                        ResourceBuilder
                            .CreateDefault()
                            .AddContainerDetector()
                            .AddOperatingSystemDetector()
                    )
                    .AddSource("Wolverine")
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddNpgsql()
                    .AddOtlpExporter()
            );
        
        return builder;
    }
}
