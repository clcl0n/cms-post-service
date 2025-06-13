using System;

namespace Cms.PostService.Application.Contracts.Commands.Post;

public sealed record PostBulkSitemapDataResponseUrl(
    Guid EntityId,
    string Path,
    DateTime LastModified
);
