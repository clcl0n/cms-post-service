using System.Text.Json.Serialization;

namespace Cms.PostService.Domain.Constants;

[JsonConverter(typeof(JsonStringEnumConverter<PostStatus>))]
public enum PostStatus
{
    Draft = 0,
    ReadyToReview = 2,
    UnderReview = 3,
    Scheduled = 4,
    Published = 5,
}
