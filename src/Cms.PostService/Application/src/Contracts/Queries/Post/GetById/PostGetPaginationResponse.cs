using System.Collections.Generic;

namespace Cms.PostService.Application.Contracts.Queries.Post.GetById;

public record PostGetPaginationResponse(
    int Page,
    int PageSize,
    int TotalCount,
    IEnumerable<PostGetPaginationResponseItem> Items
);
