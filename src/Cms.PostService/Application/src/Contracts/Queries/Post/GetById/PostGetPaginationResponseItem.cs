using System;
using Cms.PostService.Domain.Constants;

namespace Cms.PostService.Application.Contracts.Queries.Post.GetById;

public record PostGetPaginationResponseItem(
    Guid Id,
    string Title,
    string TopicTitle,
    PostStatus Status
);
