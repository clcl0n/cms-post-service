using System.Collections.Generic;
using Cms.PostService.Application.Contracts.Dtos;

namespace Cms.PostService.Application.Contracts.Queries;

public sealed record PostSitemapDataQueryResponse(
    int Offset,
    int Limit,
    int TotalCount,
    List<SitemapUrlDto> Urls
);
