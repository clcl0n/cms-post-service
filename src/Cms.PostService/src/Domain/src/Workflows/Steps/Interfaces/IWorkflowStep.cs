namespace Cms.PostService.Domain.Workflows.Steps.Interfaces;

public interface IWorkflowStep<T>
{
    bool WasInvoked(T data);

    void InvokeNext(T data);

    void InvokeBack(T data);
}
