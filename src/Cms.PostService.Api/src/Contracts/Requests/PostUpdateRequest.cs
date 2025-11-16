using System;
using System.Collections.Generic;
using Cms.PostService.Api.Contracts.Dtos;

namespace Cms.PostService.Api.Contracts.Requests;

public record PostUpdateRequest(
    string Title,
    Guid ListingImageId,
    Guid TopicId,
    List<BaseBodyBlockDto> BodyBlocks
);
