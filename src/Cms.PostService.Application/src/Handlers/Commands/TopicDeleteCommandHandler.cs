using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Commands;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Cms.PostService.Infrastructure.Contracts.Commands;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;
using Cms.PostService.Infrastructure.Services.Interfaces;

namespace Cms.PostService.Application.Handlers.Commands;

internal sealed class TopicDeleteCommandHandler(IRouteService routeService, IUnitOfWork unitOfWork)
    : ITopicDeleteCommandHandler
{
    public async Task<bool> HandleAsync(TopicDeleteCommand request, CancellationToken cancellationToken)
    {
        var topic = await unitOfWork.TopicRepository.GetByIdAsync(request.Id, withTracking: true, cancellationToken);

        if (topic == null)
        {
            return false;
        }

        await unitOfWork.TopicRepository.DeleteAsync(topic, cancellationToken);

        foreach (var route in topic.Routes)
        {
            await routeService.DeleteTopicRouteAsync(new DeleteTopicRouteCommand(route.Id), cancellationToken);
        }

        return true;
    }
}
