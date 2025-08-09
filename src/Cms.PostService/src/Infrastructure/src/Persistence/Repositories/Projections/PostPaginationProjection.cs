using System;

namespace Cms.PostService.Infrastructure.Persistence.Repositories.Projections;

public sealed class PostPaginationProjection
{
    public required Guid Id { get; set; }

    public required string Title { get; set; }

    public required string BodyPlainText { get; set; }

    public required Guid TopicId { get; set; }
}
