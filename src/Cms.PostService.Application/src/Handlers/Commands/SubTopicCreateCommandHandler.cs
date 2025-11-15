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

internal sealed class SubTopicCreateCommandHandler(
    IRouteService routeService,
    IUnitOfWork unitOfWork
) : ISubTopicCreateCommandHandler
{
    public async Task<SubTopicCreateCommandResponse> HandleAsync(
        SubTopicCreateCommand request,
        CancellationToken cancellationToken
    )
    {
        var slug = SlugFactory.Create(request.Title);
        var route = await routeService.CreateTopicRouteAsync(new CreateTopicRouteCommand(slug), cancellationToken);

        var subtopic = new SubTopic
        {
            Id = default,
            Title = request.Title,
            Slug = slug,
            ParentTopicId = request.ParentTopicId,
            ParentTopic = null,
            Routes = [new Route { Id = route.Id, CreatedAt = DateTime.UtcNow }],
        };

        await unitOfWork.SubTopicRepository.InsertAsync(subtopic, cancellationToken);

        return new SubTopicCreateCommandResponse(subtopic.Id, subtopic.Title);
    }
}
