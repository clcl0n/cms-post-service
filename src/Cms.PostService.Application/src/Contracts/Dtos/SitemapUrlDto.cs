using System;

namespace Cms.PostService.Application.Contracts.Dtos;

public sealed record SitemapUrlDto(
    Guid EntityId,
    string Path,
    DateTime LastModified
);
