using System;
using System.Collections.Generic;

namespace Cms.PostService.Domain.Entities;

public class SubTopic : BaseEntity
{
    public required Guid ParentTopicId { get; set; }

    public required string Title { get; set; }

    public required string Slug { get; set; }

    public required List<Route> Routes { get; set; } = [];

    public required Topic? ParentTopic { get; set; }
}
