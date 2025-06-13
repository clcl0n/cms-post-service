using System;

namespace Cms.PostService.Domain.Entities;

public class BaseEntity
{
    public required Guid Id { get; set; }
}
