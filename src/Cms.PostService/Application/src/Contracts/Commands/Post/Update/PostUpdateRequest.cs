using System;
using System.Collections.Generic;

namespace Cms.PostService.Application.Contracts.Commands.Post.Update;

public record PostUpdateRequest(
    Guid Id,
    string Title,
    Guid ListingImageId,
    Guid TopicId,
    List<PostUpdateRequestBaseBodyBlock> BodyBlocks
);
