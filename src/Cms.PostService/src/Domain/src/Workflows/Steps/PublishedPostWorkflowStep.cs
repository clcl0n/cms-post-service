using System;
using Cms.PostService.Domain.Constants;
using Cms.PostService.Domain.Entities;
using Cms.PostService.Domain.Workflows.Steps.Interfaces;

namespace Cms.PostService.Domain.Workflows.Steps;

internal sealed class PublishedPostWorkflowStep : IWorkflowStep<Post>
{
    public bool WasInvoked(Post data)
    {
        return data.Status != PostStatus.Published;
    }

    public void InvokeNext(Post data)
    {
        throw new InvalidOperationException("Published post cannot be published again.");
    }

    public void InvokeBack(Post data)
    {
        data.Status = PostStatus.Draft;
    }
}
