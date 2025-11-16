using System.Collections.Generic;

namespace Cms.PostService.Api.Contracts.Dtos;

public abstract record PaginationDto<T>(
    int Page,
    int PageSize,
    int TotalCount,
    List<T> Items
);
