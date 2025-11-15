using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Dtos;
using Cms.PostService.Application.Contracts.Queries;
using Cms.PostService.Application.Handlers.Queries.Interfaces;
using Cms.PostService.Domain.Entities.BodyBlocks;
using Cms.PostService.Infrastructure.Persistence.Repositories.Projections;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;
using Cms.PostService.Infrastructure.Services.Interfaces;

namespace Cms.PostService.Application.Handlers.Queries;

internal sealed class PostGetByIdQueryHandler(IImageService imageService, IUnitOfWork unitOfWork)
    : IPostGetByIdQueryHandler
{
    public async Task<PostGetByIdQueryResponse?> HandleAsync(
        PostGetByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await unitOfWork.PostRepository.GetByIdWithDetailsAsync(
            request.Id,
            cancellationToken
        );

        if (result is null)
        {
            return null;
        }

        return await ToTopicGetByIdResponseAsync(result, cancellationToken);
    }

    private async Task<PostGetByIdQueryResponse> ToTopicGetByIdResponseAsync(
        PostWithDetailsProjection post,
        CancellationToken cancellationToken
    )
    {
        var image =
            await imageService.GetByIdAsync(post.ListingImageId, cancellationToken)
            ?? throw new Exception(
                string.Format("Image with id {0} not found", post.ListingImageId)
            );

        return new PostGetByIdQueryResponse(
            post.Id,
            post.BodyPlainText,
            post.Title,
            new ImageDto(image.Id, image.FileName, image.SizeInBytes, image.Format),
            new TopicDto(post.Topic.Id, post.Topic.Title),
            [
                .. post.BodyBlocks.Select(block =>
                {
                    return block switch
                    {
                        ParagraphBodyBlock paragraphBlock => (BaseBodyBlockDto)
                            new ParagraphBodyBlockDto(
                                paragraphBlock.Order,
                                paragraphBlock.Content
                            ),
                        _ => throw new NotImplementedException(),
                    };
                }),
            ]
        );
    }
}
