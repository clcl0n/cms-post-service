using System;
using System.Collections.Generic;
using System.Linq;
using Cms.PostService.Domain.Constants;
using Cms.PostService.Domain.Entities.BodyBlocks;
using Cms.PostService.Domain.Events;

namespace Cms.PostService.Domain.Entities;

public class Post : BaseEntity
{
    public required string BodyPlainText { get; set; }

    public required string Title { get; set; }

    public required string Slug { get; set; }

    public required PostStatus Status { get; set; }

    public required DateTime LastModified { get; set; }

    public required Guid ListingImageId { get; set; }

    public required List<Route> Routes { get; set; } = [];

    public required Guid TopicId { get; set; }

    public virtual Topic? Topic { get; set; }

    public virtual ICollection<BaseBodyBlock> BodyBlocks { get; set; } = [];
}
