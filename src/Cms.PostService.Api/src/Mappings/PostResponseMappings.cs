using System.Linq;
using Cms.PostService.Api.Contracts.Responses;
using Cms.PostService.Application.Contracts.Commands;
using Cms.PostService.Application.Contracts.Queries;

namespace Cms.PostService.Api.Mappings;

internal static class PostResponseMappings
{
    public static PostUpdateResponse? ToPostUpdateResponse(PostUpdateCommandResponse? data)
    {
        if (data is null)
        {
            return null;
        }

        return new(
            data.Id,
            data.Title,
            data.ListingImageId,
            data.TopicId,
            [.. data.BodyBlocks.Select(DtoMappings.ToBodyBlockDto)]
        );
    }

    public static PostCreateResponse ToPostCreateResponse(PostCreateCommandResponse data)
    {
        return new(
            data.Id,
            data.Title,
            data.ListingImageId,
            data.TopicId,
            [.. data.BodyBlocks.Select(DtoMappings.ToBodyBlockDto)]
        );
    }

    public static PostGetPaginationResponse ToPostGetPaginationResponse(PostGetPaginationQueryResponse data)
    {
        return new(
            data.Page,
            data.PageSize,
            data.TotalCount,
            [.. data.Items.Select(ToPostGetPaginationResponseItem)]
        );
    }

    public static PostGetByIdResponse? ToPostGetByIdResponse(PostGetByIdQueryResponse? data)
    {
        if (data is null)
        {
            return null;
        }

        return new(
            data.Id,
            data.BodyPlainText,
            data.Title,
            DtoMappings.ToImageDto(data.ListingImage),
            DtoMappings.ToTopicDto(data.Topic),
            [.. data.BodyBlocks.Select(DtoMappings.ToBodyBlockDto)]
        );
    }

    private static PostGetPaginationResponseItem ToPostGetPaginationResponseItem(PostGetPaginationQueryResponseItem item)
    {
        return new(
            item.Id,
            item.Title,
            item.TopicTitle,
            item.Status
        );
    }
}