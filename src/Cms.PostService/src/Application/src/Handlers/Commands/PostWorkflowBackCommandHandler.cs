using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Commands.Post;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Cms.PostService.Domain.Constants;
using Cms.PostService.Domain.Workflows;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;
using Cms.PostService.Infrastructure.Services.Interfaces;

namespace Cms.PostService.Application.Handlers.Commands;

internal sealed class PostWorkflowBackCommandHandler(
    IUnitOfWork unitOfWork,
    ISitemapService sitemapService
) : IPostWorkflowBackCommandHandler
{
    public async Task<PostWorkflowBackResponse?> HandleAsync(
        PostWorkflowBackRequest request,
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

        var @event = PostWorkflow.InvokeBack(post);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        if (@event.CurrentStatus != PostStatus.Published)
        {
            await sitemapService.ScheduleDeleteUrlAsync(post.Id);
        }

        return new PostWorkflowBackResponse(post.Id, post.Status);
    }
}
