using System.Collections.Generic;

namespace Cms.PostService.Application.Contracts.Commands.Post;

public sealed record PostBulkSitemapDataResponse(
    int Offset,
    int Limit,
    int TotalCount,
    List<PostBulkSitemapDataResponseUrl> Urls
);
