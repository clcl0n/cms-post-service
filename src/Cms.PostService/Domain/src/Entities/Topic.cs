using System.Collections.Generic;

namespace Cms.PostService.Domain.Entities;

public class Topic : BaseEntity
{
    public required string Title { get; set; }

    public required string Slug { get; set; }

    public required List<Route> Routes { get; set; } = [];

    public virtual required ICollection<SubTopic> SubTopics { get; set; } = [];
}
