using System;

namespace Cms.PostService.Api.Contracts.Dtos;

public sealed record SitemapUrlDto(
    Guid EntityId,
    string Path,
    DateTime LastModified
);
