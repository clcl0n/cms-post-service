using System;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Commands;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Cms.PostService.Domain.Entities;
using Cms.PostService.Domain.Factories;
using Cms.PostService.Infrastructure.Contracts.Commands;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;
using Cms.PostService.Infrastructure.Services.Interfaces;

namespace Cms.PostService.Application.Handlers.Commands;

internal sealed class TopicCreateCommandHandler(IRouteService routeService, IUnitOfWork unitOfWork)
    : ITopicCreateCommandHandler
{
    public async Task<TopicCreateCommandResponse> HandleAsync(
        TopicCreateCommand request,
        CancellationToken cancellationToken
    )
    {
        var slug = SlugFactory.Create(request.Title);
        var route = await routeService.CreateTopicRouteAsync(new CreateTopicRouteCommand(slug), cancellationToken);

        var topic = new Topic
        {
            Id = default,
            Title = request.Title,
            Slug = slug,
            Routes = [new Route { Id = route.Id, CreatedAt = DateTime.UtcNow }],
            SubTopics = [],
        };

        await unitOfWork.TopicRepository.InsertAsync(topic, cancellationToken);

        return new TopicCreateCommandResponse(topic.Id, topic.Title);
    }
}
