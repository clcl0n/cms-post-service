using System;
using System.Linq;
using Cms.PostService.Api.Contracts.Dtos;
using Cms.PostService.Api.Contracts.Requests;
using Cms.PostService.Application.Contracts.Commands;

namespace Cms.PostService.Api.Mappings;

internal static class PostRequestMappings
{
    public static PostCreateCommand ToPostCreateCommand(PostCreateRequest request)
    {
        return new(
            request.Title,
            request.ListingImageId,
            request.TopicId,
            [.. request.BodyBlocks.Select(ToBodyBlockDto)]
        );
    }

    public static PostUpdateCommand ToPostUpdateCommand(PostUpdateRequest request, Guid id)
    {
        return new(
            id,
            request.Title,
            request.ListingImageId,
            request.TopicId,
            [.. request.BodyBlocks.Select(ToBodyBlockDto)]
        );
    }

    private static Application.Contracts.Dtos.BaseBodyBlockDto ToBodyBlockDto(BaseBodyBlockDto request)
    {
        return request switch
        {
            ParagraphBodyBlockDto paragraphBlock => new Application.Contracts.Dtos.ParagraphBodyBlockDto(paragraphBlock.Order, paragraphBlock.Content),
            _ => throw new NotImplementedException(),
        };
    }
}