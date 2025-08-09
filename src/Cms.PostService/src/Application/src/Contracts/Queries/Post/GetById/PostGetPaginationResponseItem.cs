using System;

namespace Cms.PostService.Application.Contracts.Queries.Post.GetById;

public record PostGetPaginationResponseItem(
    Guid Id,
    string Title,
    string BodyPlainText,
    Guid TopicId
);
