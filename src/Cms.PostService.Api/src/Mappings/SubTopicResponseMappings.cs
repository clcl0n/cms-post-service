using Cms.PostService.Api.Contracts.Responses;
using Cms.PostService.Application.Contracts.Commands;
using Cms.PostService.Application.Contracts.Queries;

namespace Cms.PostService.Api.Mappings;

internal static class SubTopicResponseMappings
{
    public static SubTopicGetByIdResponse ToSubTopicGetByIdResponse(SubTopicGetByIdQueryResponse data)
    {
        return new (data.Id, data.Title);
    }

    public static SubTopicCreateResponse ToSubTopicCreateResponse(SubTopicCreateCommandResponse data)
    {
        return new (data.Id, data.Title);
    }

    public static SubTopicUpdateResponse ToSubTopicUpdateResponse(SubTopicUpdateCommandResponse data)
    {
        return new (data.Id, data.Title);
    }
}