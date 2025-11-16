using System;
using Cms.PostService.Domain.Constants;

namespace Cms.PostService.Api.Contracts.Responses;

public record PostGetPaginationResponseItem(
    Guid Id,
    string Title,
    string TopicTitle,
    PostStatus Status
);
