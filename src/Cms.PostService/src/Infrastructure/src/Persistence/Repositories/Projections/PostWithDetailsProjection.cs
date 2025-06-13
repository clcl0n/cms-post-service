using System;
using System.Collections.Generic;
using Cms.PostService.Domain.Entities.BodyBlocks;

namespace Cms.PostService.Infrastructure.Persistence.Repositories.Projections;

public sealed class PostWithDetailsProjection
{
    public required Guid Id { get; set; }

    public required string Title { get; set; }

    public required string BodyPlainText { get; set; }

    public required Guid ListingImageId { get; set; }

    public required PostWithDetailsProjectionTopic Topic { get; set; }

    public required List<BaseBodyBlock> BodyBlocks { get; set; } = [];
}
