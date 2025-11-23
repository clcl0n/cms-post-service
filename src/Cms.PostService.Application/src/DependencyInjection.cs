using Cms.PostService.Application.Handlers.Commands;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Cms.PostService.Application.Handlers.Queries;
using Cms.PostService.Application.Handlers.Queries.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.PostService.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISubTopicCreateCommandHandler, SubTopicCreateCommandHandler>();
        services.AddScoped<ISubTopicUpdateCommandHandler, SubTopicUpdateCommandHandler>();
        services.AddScoped<ISubTopicDeleteCommandHandler, SubTopicDeleteCommandHandler>();

        services.AddScoped<ITopicCreateCommandHandler, TopicCreateCommandHandler>();
        services.AddScoped<ITopicUpdateCommandHandler, TopicUpdateCommandHandler>();
        services.AddScoped<ITopicDeleteCommandHandler, TopicDeleteCommandHandler>();
        services.AddScoped<ITopicGetByIdsQueryHandler, TopicGetByIdsQueryHandler>();

        services.AddScoped<IPostCreateCommandHandler, PostCreateCommandHandler>();
        services.AddScoped<IPostUpdateCommandHandler, PostUpdateCommandHandler>();
        services.AddScoped<IPostDeleteCommandHandler, PostDeleteCommandHandler>();
        services.AddScoped<IPostGetByIdQueryHandler, PostGetByIdQueryHandler>();
        services.AddScoped<IPostWorkflowBackCommandHandler, PostWorkflowBackCommandHandler>();
        services.AddScoped<IPostWorkflowNextCommandHandler, PostWorkflowNextCommandHandler>();
        services.AddScoped<IPostSitemapDataCommandHandler, PostSitemapDataCommandHandler>();
        services.AddScoped<IPostGetPaginationQueryHandler, PostGetPaginationQueryHandler>();
    }
}
