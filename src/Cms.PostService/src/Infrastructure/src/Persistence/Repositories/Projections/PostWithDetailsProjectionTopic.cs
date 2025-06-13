using System;

namespace Cms.PostService.Infrastructure.Persistence.Repositories.Projections;

public sealed class PostWithDetailsProjectionTopic
{
    public required Guid Id { get; set; }

    public required string Title { get; set; }
}
