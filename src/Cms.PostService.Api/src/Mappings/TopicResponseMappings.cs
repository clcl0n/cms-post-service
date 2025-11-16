using Cms.PostService.Api.Contracts.Responses;
using Cms.PostService.Application.Contracts.Commands;
using Cms.PostService.Application.Contracts.Queries;

namespace Cms.PostService.Api.Mappings;

internal static class TopicResponseMappings
{
    public static TopicGetByIdResponse ToTopicGetByIdResponse(TopicGetByIdQueryResponse data)
    {
        return new (data.Id, data.Title);
    }

    public static TopicCreateResponse ToTopicCreateResponse(TopicCreateCommandResponse data)
    {
        return new (data.Id, data.Title);
    }

    public static TopicUpdateResponse ToTopicUpdateResponse(TopicUpdateCommandResponse data)
    {
        return new (data.Id, data.Title);
    }
}