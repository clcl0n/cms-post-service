using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Commands;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Cms.PostService.Infrastructure.Contracts.Commands;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;
using Cms.PostService.Infrastructure.Services.Interfaces;

namespace Cms.PostService.Application.Handlers.Commands;

internal sealed class SubTopicDeleteCommandHandler(
    IRouteService routeService,
    IUnitOfWork unitOfWork
) : ISubTopicDeleteCommandHandler
{
    public async Task<bool> HandleAsync(
        SubTopicDeleteCommand request,
        CancellationToken cancellationToken
    )
    {
        var topic = await unitOfWork.SubTopicRepository.GetByIdAsync(request.Id, withTracking: true, cancellationToken);

        if (topic == null)
        {
            return false;
        }

        await unitOfWork.SubTopicRepository.DeleteAsync(topic, cancellationToken);

        foreach (var route in topic.Routes)
        {
            await routeService.DeleteTopicRouteAsync(new DeleteTopicRouteCommand(route.Id), cancellationToken);
        }

        return true;
    }
}
