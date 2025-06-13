using System;

namespace Cms.PostService.Domain.Entities;

public class Route
{
    public required Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }
}
