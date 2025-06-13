using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Cms.PostService.Api.Extensions;

public static class OtelExtension
{
    public static void ConfigureOtel(this IServiceCollection services)
    {
        services
            .AddOpenTelemetry()
            .WithMetrics(metrics =>
                metrics
                    .SetResourceBuilder(
                        ResourceBuilder.CreateDefault().AddOperatingSystemDetector()
                    )
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter()
            )
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
    }

    public static void ConfigureOtel(this ILoggingBuilder logging)
    {
        logging.AddOpenTelemetry(options =>
        {
            options.IncludeFormattedMessage = true;

            options
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddOperatingSystemDetector())
                .AddOtlpExporter();
        });
    }
}
