using Cms.PostService.Application.Handlers.Commands;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Cms.PostService.Application.Handlers.Queries;
using Cms.PostService.Application.Handlers.Queries.Interfaces;
using Cms.PostService.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.PostService.Application;

public static class DependencyInjection
{
    public static void AddApplication(
        this IServiceCollection services,
        IConfiguration configuration,
        IHealthChecksBuilder? healthChecksBuilder = null
    )
    {
        services.AddInfrastructure(healthChecksBuilder, configuration);

        services.AddScoped<ISubTopicCreateCommandHandler, SubTopicCreateCommandHandler>();
        services.AddScoped<ISubTopicUpdateCommandHandler, SubTopicUpdateCommandHandler>();
        services.AddScoped<ISubTopicDeleteCommandHandler, SubTopicDeleteCommandHandler>();
        services.AddScoped<ISubTopicGetByIdQueryHandler, SubTopicGetByIdQueryHandler>();

        services.AddScoped<ITopicCreateCommandHandler, TopicCreateCommandHandler>();
        services.AddScoped<ITopicUpdateCommandHandler, TopicUpdateCommandHandler>();
        services.AddScoped<ITopicDeleteCommandHandler, TopicDeleteCommandHandler>();
        services.AddScoped<ITopicGetByIdQueryHandler, TopicGetByIdQueryHandler>();

        services.AddScoped<IPostCreateCommandHandler, PostCreateCommandHandler>();
        services.AddScoped<IPostUpdateCommandHandler, PostUpdateCommandHandler>();
        services.AddScoped<IPostDeleteCommandHandler, PostDeleteCommandHandler>();
        services.AddScoped<IPostGetByIdQueryHandler, PostGetByIdQueryHandler>();
        services.AddScoped<IPostWorkflowBackCommandHandler, PostWorkflowBackCommandHandler>();
        services.AddScoped<IPostWorkflowNextCommandHandler, PostWorkflowNextCommandHandler>();
        services.AddScoped<IPostBulkSitemapDataCommandHandler, PostBulkSitemapDataCommandHandler>();
        services.AddScoped<IPostGetPaginationQueryHandler, PostGetPaginationQueryHandler>();
    }
}
