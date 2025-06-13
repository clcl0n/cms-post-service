using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Commands.Post;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Cms.PostService.Domain.Constants;
using Cms.PostService.Domain.Workflows;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;
using Cms.PostService.Infrastructure.Services.Interfaces;

namespace Cms.PostService.Application.Handlers.Commands;

internal sealed class PostWorkflowNextCommandHandler(
    IUnitOfWork unitOfWork,
    ISitemapService sitemapService,
    IRouteService routeService
) : IPostWorkflowNextCommandHandler
{
    public async Task<PostWorkflowNextResponse?> HandleAsync(
        PostWorkflowNextRequest request,
        CancellationToken cancellationToken
    )
    {
        var post = await unitOfWork.PostRepository.GetByIdForWorkflowAsTrackingAsync(
            request.Id,
            cancellationToken
        );

        if (post is null)
        {
            return null;
        }

        var @event = PostWorkflow.InvokeNext(post);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        if (@event.CurrentStatus == PostStatus.Published)
        {
            var latestRoute = post.Routes.OrderByDescending(route => route.CreatedAt).First();

            var route = await routeService.GetPostRouteByIdAsync(latestRoute.Id, cancellationToken);

            await sitemapService.ScheduleUpsertUrlAsync(post.Id, route.FullPath, post.LastModified);
        }

        return new PostWorkflowNextResponse(post.Id, post.Status);
    }
}
