using System;
using Cms.PostService.Domain.Constants;
using Cms.PostService.Domain.Entities;
using Cms.PostService.Domain.Workflows.Steps.Interfaces;

namespace Cms.PostService.Domain.Workflows.Steps;

internal sealed class DraftPostWorkflowStep : IWorkflowStep<Post>
{
    public bool WasInvoked(Post data)
    {
        return data.Status != PostStatus.Draft;
    }

    public void InvokeNext(Post data)
    {
        data.Status = PostStatus.ReadyToReview;
    }

    public void InvokeBack(Post data)
    {
        throw new InvalidOperationException("Cannot go back from DraftPostWorkflowStep");
    }
}
