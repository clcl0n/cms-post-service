using Cms.PostService.Domain.Constants;
using Cms.PostService.Domain.Entities;
using Cms.PostService.Domain.Workflows.Steps.Interfaces;

namespace Cms.PostService.Domain.Workflows.Steps;

internal sealed class UnderReviewPostWorkflowStep : IWorkflowStep<Post>
{
    public bool WasInvoked(Post data)
    {
        return data.Status != PostStatus.UnderReview;
    }

    public void InvokeNext(Post data)
    {
        data.Status = PostStatus.Published;

        // TODO: Scheduled
    }

    public void InvokeBack(Post data)
    {
        data.Status = PostStatus.Draft;
    }
}
