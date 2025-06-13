using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Commands.Post.Update;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Cms.PostService.Domain.Builders;
using Cms.PostService.Domain.Entities;
using Cms.PostService.Domain.Entities.BodyBlocks;
using Cms.PostService.Domain.Factories;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;
using Cms.PostService.Infrastructure.Services.Interfaces;

namespace Cms.PostService.Application.Handlers.Commands;

internal sealed class PostUpdateCommandHandler(IUnitOfWork unitOfWork, IRouteService routeService)
    : IPostUpdateCommandHandler
{
    public async Task<PostUpdateResponse?> HandleAsync(
        PostUpdateRequest request,
        CancellationToken cancellationToken
    )
    {
        await EnsurePostCanBeUpdatedAsync(request.Id, cancellationToken);

        var response = await unitOfWork.PostRepository.GetByIdForUpdateAsTrackingAsync(
            request.Id,
            cancellationToken
        );

        if (response is null)
        {
            return null;
        }

        await UpdateExistingPostAsync(response, request, cancellationToken);

        return ToPostUpdateResponse(response);
    }

    private async Task UpdateExistingPostAsync(
        Post? existingEntity,
        PostUpdateRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(existingEntity, nameof(existingEntity));

        if (existingEntity.Title != request.Title)
        {
            existingEntity.Slug = SlugFactory.Create(request.Title);

            var newRoute = await routeService.CreatePostRouteAsync(
                existingEntity.Slug,
                cancellationToken
            );
            existingEntity.Routes.Add(new Route { Id = newRoute.Id, CreatedAt = DateTime.UtcNow });
        }

        existingEntity.LastModified = DateTime.UtcNow;
        existingEntity.Title = request.Title;
        existingEntity.TopicId = request.TopicId;
        existingEntity.ListingImageId = request.ListingImageId;
        existingEntity.BodyBlocks.Clear();
        existingEntity.BodyBlocks =
        [
            .. request.BodyBlocks.Select(block =>
            {
                return block switch
                {
                    PostUpdateRequestParagraphBodyBlock paragraphBlock => (BaseBodyBlock)
                        new ParagraphBodyBlock
                        {
                            Order = paragraphBlock.Order,
                            Content = paragraphBlock.Content,
                        },
                    _ => throw new NotImplementedException(),
                };
            }),
        ];
        existingEntity.BodyPlainText = GetBodyPlainText(existingEntity.BodyBlocks);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static string GetBodyPlainText(IEnumerable<BaseBodyBlock> bodyBlocks)
    {
        var bodyPlainTextBuilder = new BodyPlainTextBuilder();

        foreach (var bodyBlock in bodyBlocks)
        {
            bodyPlainTextBuilder.Append(bodyBlock);
        }

        return bodyPlainTextBuilder.GetResult();
    }

    private static PostUpdateResponse ToPostUpdateResponse(Post post)
    {
        return new PostUpdateResponse(
            post.Id,
            post.Title,
            post.ListingImageId,
            post.TopicId,
            [
                .. post.BodyBlocks.Select(block =>
                {
                    return block switch
                    {
                        ParagraphBodyBlock paragraphBlock => (PostUpdateResponseBaseBodyBlock)
                            new PostUpdateResponseParagraphBodyBlock(
                                paragraphBlock.Order,
                                paragraphBlock.Content
                            ),
                        _ => throw new NotImplementedException(),
                    };
                }),
            ]
        );
    }

    private async Task EnsurePostCanBeUpdatedAsync(Guid id, CancellationToken cancellationToken)
    {
        if (await unitOfWork.PostRepository.IsPublishedAsync(id, cancellationToken))
        {
            throw new InvalidOperationException("Cannot update a published post.");
        }
    }
}
