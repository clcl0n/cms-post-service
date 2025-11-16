using System;
using System.Collections.Generic;
using Cms.PostService.Api.Contracts.Dtos;

namespace Cms.PostService.Api.Contracts.Responses;

public record PostCreateResponse(
    Guid Id,
    string Title,
    Guid ListingImageId,
    Guid TopicId,
    List<BaseBodyBlockDto> BodyBlocks
);
