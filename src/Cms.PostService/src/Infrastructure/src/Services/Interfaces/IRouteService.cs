using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Infrastructure.Contracts.Commands;
using Cms.PostService.Infrastructure.Contracts.Queries;

namespace Cms.PostService.Infrastructure.Services.Interfaces;

public interface IRouteService
{
    Task<CreateTopicRouteCommandResult> CreateTopicRouteAsync(
        CreateTopicRouteCommand command,
        CancellationToken cancellationToken
    );

    Task<CreatePostRouteCommandResult> CreatePostRouteAsync(
        CreatePostRouteCommand command,
        CancellationToken cancellationToken
    );

    Task<TopicRouteByIdQueryResult> GetTopicRouteByIdAsync(
        TopicRouteByIdQuery query,
        CancellationToken cancellationToken
    );

    Task<PostRouteByIdQueryResult> GetPostRouteByIdAsync(
        PostRouteByIdQuery query,
        CancellationToken cancellationToken
    );

    Task DeleteTopicRouteAsync(DeleteTopicRouteCommand command, CancellationToken cancellationToken);

    Task DeletePostRouteAsync(DeletePostRouteCommand command, CancellationToken cancellationToken);
}
