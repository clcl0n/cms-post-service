using System;
using Cms.PostService.Infrastructure.Persistence;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;
using Cms.PostService.Infrastructure.Services;
using Cms.PostService.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using static Cms.Protos.PostRouteService;
using static Cms.Protos.TopicRouteService;

namespace Cms.PostService.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IHealthChecksBuilder? healthChecksBuilder,
        IConfiguration configuration
    )
    {
        healthChecksBuilder?.AddInfrastructureHealthChecks(configuration);

        services.AddDbContext(configuration);
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddGrpcClient<PostRouteServiceClient>(o =>
        {
            // TODO: konfiguracia
            o.Address = new Uri("http://localhost:5022");
        })
        .AddStandardResilienceHandler();
        services.AddGrpcClient<TopicRouteServiceClient>(o =>
        {
            // TODO: konfiguracia
            o.Address = new Uri("http://localhost:5022");
        })
        .AddStandardResilienceHandler();

        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IRouteService, RouteService>();
        services.AddScoped<ISitemapService, SitemapService>();
        services.AddScoped<IPersistenceService, PersistenceService>();
    }

    public static void AddCliInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext(configuration);

        services.AddScoped<IPersistenceService, PersistenceService>();
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DbContext, PostServiceDbContext>(
            (provider, opts) =>
            {
                opts.UseNpgsql(
                        configuration.GetConnectionString("Postgres"),
                        x => x.MigrationsHistoryTable("__EFMigrationsHistory", "cms-post-service")
                    )
                    .UseSnakeCaseNamingConvention()
                    .ConfigureWarnings(x =>
                    {
                        x.Ignore(
                            CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning
                        );
                    });
            }
        );
    }

    private static IHealthChecksBuilder AddInfrastructureHealthChecks(
        this IHealthChecksBuilder builder,
        IConfiguration configuration
    )
    {
        builder.AddNpgSql(
            configuration.GetConnectionString("Postgres")
                ?? throw new NullReferenceException("Postgres connection string was not found.")
        );

        return builder;
    }
}
