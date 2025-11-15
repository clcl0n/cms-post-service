using System;

namespace Cms.PostService.Infrastructure.Persistence.Repositories.Projections;

public sealed class PostSitemapInfoProjection
{
    public required Guid Id { get; set; }

    public required DateTime LastModified { get; set; }

    public required string Path { get; set; }
}
