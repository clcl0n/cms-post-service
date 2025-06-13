using System;
using System.Threading;
using System.Threading.Tasks;
using Cms.Contracts.Routes;
using Cms.PostService.Infrastructure.Services.Interfaces;
using Wolverine;

namespace Cms.PostService.Infrastructure.Services;

internal sealed class RouteService(IMessageBus bus) : IRouteService
{
    public Task<TopicRouteCreateResponse> CreateTopicRouteAsync(
        string slug,
        CancellationToken cancellationToken
    )
    {
        return bus.InvokeAsync<TopicRouteCreateResponse>(
            new TopicRouteCreateRequest(slug),
            cancellationToken
        );
    }

    public Task<PostRouteCreateResponse> CreatePostRouteAsync(
        string slug,
        CancellationToken cancellationToken
    )
    {
        return bus.InvokeAsync<PostRouteCreateResponse>(
            new PostRouteCreateRequest(slug),
            cancellationToken
        );
    }

    public Task<TopicRouteGetByIdResponse> GetTopicRouteByIdAsync(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        return bus.InvokeAsync<TopicRouteGetByIdResponse>(
            new TopicRouteGetByIdRequest(id),
            cancellationToken
        );
    }

    public Task<PostRouteGetByIdResponse> GetPostRouteByIdAsync(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        return bus.InvokeAsync<PostRouteGetByIdResponse>(
            new PostRouteGetByIdRequest(id),
            cancellationToken
        );
    }

    public Task DeleteTopicRouteAsync(Guid id, CancellationToken cancellationToken)
    {
        return bus.InvokeAsync(new TopicRouteDeleteRequest(id), cancellationToken);
    }

    public Task DeletePostRouteAsync(Guid id, CancellationToken cancellationToken)
    {
        return bus.InvokeAsync(new PostRouteDeleteRequest(id), cancellationToken);
    }
}
