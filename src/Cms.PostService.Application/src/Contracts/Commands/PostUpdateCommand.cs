using System;
using System.Collections.Generic;
using Cms.PostService.Application.Contracts.Dtos;

namespace Cms.PostService.Application.Contracts.Commands;

public record PostUpdateCommand(
    Guid Id,
    string Title,
    Guid ListingImageId,
    Guid TopicId,
    List<BaseBodyBlockDto> BodyBlocks
);
