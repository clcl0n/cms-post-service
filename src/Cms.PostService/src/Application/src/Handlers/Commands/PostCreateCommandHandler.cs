using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Commands.Post.Create;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Cms.PostService.Domain.Builders;
using Cms.PostService.Domain.Constants;
using Cms.PostService.Domain.Entities;
using Cms.PostService.Domain.Entities.BodyBlocks;
using Cms.PostService.Domain.Factories;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;
using Cms.PostService.Infrastructure.Services.Interfaces;

namespace Cms.PostService.Application.Handlers.Commands;

internal sealed class PostCreateCommandHandler(IUnitOfWork unitOfWork, IRouteService routeService)
    : IPostCreateCommandHandler
{
    public async Task<PostCreateResponse> HandleAsync(
        PostCreateRequest request,
        CancellationToken cancellationToken
    )
    {
        var bodyBlocks = ToBodyBlocks(request.BodyBlocks);
        var bodyPlainText = GetBodyPlainText(bodyBlocks);

        var slug = SlugFactory.Create(request.Title);
        var route = await routeService.CreatePostRouteAsync(slug, cancellationToken);

        var newPost = new Post
        {
            Id = default,
            Title = request.Title,
            ListingImageId = request.ListingImageId,
            TopicId = request.TopicId,
            Status = PostStatus.Draft,
            BodyBlocks = bodyBlocks,
            BodyPlainText = bodyPlainText,
            LastModified = DateTime.UtcNow,
            Slug = slug,
            Routes = [new Route { Id = route.Id, CreatedAt = DateTime.UtcNow }],
        };

        await unitOfWork.PostRepository.InsertAsync(newPost, cancellationToken);

        return ToPostCreateResponse(newPost);
    }

    private static List<BaseBodyBlock> ToBodyBlocks(
        List<PostCreateRequestBaseBodyBlock> requestBodyBlocks
    )
    {
        return
        [
            .. requestBodyBlocks.Select(block =>
            {
                return block switch
                {
                    PostCreateRequestParagraphBodyBlock paragraphBlock => (BaseBodyBlock)
                        new ParagraphBodyBlock
                        {
                            Order = paragraphBlock.Order,
                            Content = paragraphBlock.Content,
                        },
                    _ => throw new NotImplementedException(),
                };
            }),
        ];
    }

    private static string GetBodyPlainText(List<BaseBodyBlock> bodyBlocks)
    {
        var bodyPlainTextBuilder = new BodyPlainTextBuilder();

        foreach (var bodyBlock in bodyBlocks)
        {
            bodyPlainTextBuilder.Append(bodyBlock);
        }

        return bodyPlainTextBuilder.GetResult();
    }

    private static PostCreateResponse ToPostCreateResponse(Post post)
    {
        return new PostCreateResponse(
            post.Id,
            post.Title,
            post.ListingImageId,
            post.TopicId,
            [
                .. post.BodyBlocks.Select(block =>
                {
                    return block switch
                    {
                        ParagraphBodyBlock paragraphBlock => (PostCreateResponseBaseBodyBlock)
                            new PostCreateResponseParagraphBodyBlock(
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
