using System;
using System.Threading;
using System.Threading.Tasks;
using Cms.Contracts.Routes;

namespace Cms.PostService.Infrastructure.Services.Interfaces;

public interface IRouteService
{
    Task<TopicRouteCreateResponse> CreateTopicRouteAsync(
        string slug,
        CancellationToken cancellationToken
    );

    Task<PostRouteCreateResponse> CreatePostRouteAsync(
        string slug,
        CancellationToken cancellationToken
    );

    Task<TopicRouteGetByIdResponse> GetTopicRouteByIdAsync(
        Guid id,
        CancellationToken cancellationToken
    );

    Task<PostRouteGetByIdResponse> GetPostRouteByIdAsync(
        Guid id,
        CancellationToken cancellationToken
    );

    Task DeleteTopicRouteAsync(Guid id, CancellationToken cancellationToken);

    Task DeletePostRouteAsync(Guid id, CancellationToken cancellationToken);
}
