using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Commands.SubTopic;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;
using Cms.PostService.Infrastructure.Services.Interfaces;

namespace Cms.PostService.Application.Handlers.Commands;

internal sealed class SubTopicDeleteCommandHandler(
    IRouteService routeService,
    IUnitOfWork unitOfWork
) : ISubTopicDeleteCommandHandler
{
    public async Task HandleAsync(
        SubTopicDeleteRequest request,
        CancellationToken cancellationToken
    )
    {
        var topic = await unitOfWork.SubTopicRepository.GetByIdAsync(request.Id, cancellationToken);

        if (topic == null)
        {
            return;
        }

        await unitOfWork.SubTopicRepository.DeleteAsync(topic, cancellationToken);

        foreach (var route in topic.Routes)
        {
            await routeService.DeleteTopicRouteAsync(route.Id, cancellationToken);
        }
    }
}
