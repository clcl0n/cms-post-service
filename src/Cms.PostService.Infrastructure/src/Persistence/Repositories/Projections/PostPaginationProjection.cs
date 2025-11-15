using System;
using Cms.PostService.Domain.Constants;

namespace Cms.PostService.Infrastructure.Persistence.Repositories.Projections;

public sealed class PostPaginationProjection
{
    public required Guid Id { get; set; }

    public required string Title { get; set; }

    public required PostStatus Status { get; set; }

    public required String TopicTitle { get; set; }
}
