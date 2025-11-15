using System;

namespace Cms.PostService.Domain.Entities.BodyBlocks;

public abstract class BaseBodyBlock
{
    public Guid PostId { get; set; }

    public int Order { get; set; }

    public virtual Post? Post { get; set; }
}
