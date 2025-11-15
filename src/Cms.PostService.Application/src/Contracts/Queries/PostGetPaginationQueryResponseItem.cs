using System;
using Cms.PostService.Domain.Constants;

namespace Cms.PostService.Application.Contracts.Queries;

public record PostGetPaginationQueryResponseItem(
    Guid Id,
    string Title,
    string TopicTitle,
    PostStatus Status
);
