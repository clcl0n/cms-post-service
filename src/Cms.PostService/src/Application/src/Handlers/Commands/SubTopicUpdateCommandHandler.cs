using System;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Commands.SubTopic;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Cms.PostService.Domain.Entities;
using Cms.PostService.Domain.Factories;
using Cms.PostService.Infrastructure.Contracts.Commands;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;
using Cms.PostService.Infrastructure.Services.Interfaces;

namespace Cms.PostService.Application.Handlers.Commands;

internal sealed class SubTopicUpdateCommandHandler(
    IRouteService routeService,
    IUnitOfWork unitOfWork
) : ISubTopicUpdateCommandHandler
{
    public async Task<SubTopicUpdateResponse?> HandleAsync(
        SubTopicUpdateRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await unitOfWork.SubTopicRepository.GetByIdAsTrackingAsync(
            request.Id,
            cancellationToken
        );

        if (response is null)
        {
            return null;
        }

        await UpdateExistingSubTopicAsync(response, request, cancellationToken);

        return new SubTopicUpdateResponse(response.Id, response.Title);
    }

    private async Task UpdateExistingSubTopicAsync(
        SubTopic? existingEntity,
        SubTopicUpdateRequest request,
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
