using System.Collections.Generic;
using Cms.PostService.Api.Contracts.Dtos;

namespace Cms.PostService.Api.Contracts.Responses;

public record PostGetPaginationResponse(
    int Page,
    int PageSize,
    int TotalCount,
    List<PostGetPaginationResponseItem> Items
) : PaginationDto<PostGetPaginationResponseItem>(Page, PageSize, TotalCount, Items);
