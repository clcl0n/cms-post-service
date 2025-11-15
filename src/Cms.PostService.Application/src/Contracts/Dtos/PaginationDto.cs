using System.Collections.Generic;

namespace Cms.PostService.Application.Contracts.Dtos;

public abstract record PaginationDto<T>(
    int Page,
    int PageSize,
    int TotalCount,
    List<T> Items
);
