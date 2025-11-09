using System;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Commands.Topic;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Cms.PostService.Domain.Entities;
using Cms.PostService.Domain.Factories;
using Cms.PostService.Infrastructure.Contracts.Commands;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;
using Cms.PostService.Infrastructure.Services.Interfaces;

namespace Cms.PostService.Application.Handlers.Commands;

internal sealed class TopicUpdateCommandHandler(IRouteService routeService, IUnitOfWork unitOfWork)
    : ITopicUpdateCommandHandler
{
    public async Task<TopicUpdateResponse?> HandleAsync(
        TopicUpdateRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await unitOfWork.TopicRepository.GetByIdAsTrackingAsync(
            request.Id,
            cancellationToken
        );

        if (response is null)
        {
            return null;
        }

        await UpdateExistingTopicAsync(response, request, cancellationToken);

        return new TopicUpdateResponse(response.Id, response.Title);
    }

    private async Task UpdateExistingTopicAsync(
        Topic? existingEntity,
        TopicUpdateRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(existingEntity, nameof(existingEntity));

        if (existingEntity.Title != request.Title)
        {
            existingEntity.Slug = SlugFactory.Create(request.Title);

            var newRoute = await routeService.CreateTopicRouteAsync(
                new CreateTopicRouteCommand(existingEntity.Slug),
                cancellationToken
            );
            existingEntity.Routes.Add(new Route { Id = newRoute.Id, CreatedAt = DateTime.UtcNow });
        }

        existingEntity.Title = request.Title;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
