using System;
using System.Collections.Frozen;
using System.Linq;
using Cms.PostService.Domain.Entities;
using Cms.PostService.Domain.Events;
using Cms.PostService.Domain.Workflows.Steps;
using Cms.PostService.Domain.Workflows.Steps.Interfaces;

namespace Cms.PostService.Domain.Workflows;

public static class PostWorkflow
{
    private static readonly FrozenSet<IWorkflowStep<Post>> _frozenSteps = new IWorkflowStep<Post>[]
    {
        new DraftPostWorkflowStep(),
        new ReadyToReviewPostWorkflowStep(),
        new UnderReviewPostWorkflowStep(),
        new PublishedPostWorkflowStep(),
    }.ToFrozenSet();

    public static PostWorkflowEvent InvokeNext(Post data)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));

        var previousStatus = data.Status;
        var stepToInvoke = GetStepToInvoke(data);

        stepToInvoke.InvokeNext(data);

        return new PostWorkflowEvent(data.Id, previousStatus, data.Status);
    }

    public static PostWorkflowEvent InvokeBack(Post data)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));

        var previousStatus = data.Status;
        var stepToInvoke = GetStepToInvoke(data);

        stepToInvoke.InvokeBack(data);

        return new PostWorkflowEvent(data.Id, previousStatus, data.Status);
    }

    private static IWorkflowStep<Post> GetStepToInvoke(Post data)
    {
        return _frozenSteps.FirstOrDefault(x => x.WasInvoked(data) is false)
            ?? throw new InvalidOperationException("No step found");
    }
}
