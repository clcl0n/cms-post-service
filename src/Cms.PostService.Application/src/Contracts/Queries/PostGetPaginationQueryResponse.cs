using System.Collections.Generic;
using Cms.PostService.Application.Contracts.Dtos;

namespace Cms.PostService.Application.Contracts.Queries;

public record PostGetPaginationQueryResponse(
    int Page,
    int PageSize,
    int TotalCount,
    List<PostGetPaginationQueryResponseItem> Items
) : PaginationDto<PostGetPaginationQueryResponseItem>(Page, PageSize, TotalCount, Items);
