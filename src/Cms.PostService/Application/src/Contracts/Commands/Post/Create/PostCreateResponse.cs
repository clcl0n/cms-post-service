using System;
using System.Collections.Generic;

namespace Cms.PostService.Application.Contracts.Commands.Post.Create;

public record PostCreateResponse(
    Guid Id,
    string Title,
    Guid ListingImageId,
    Guid TopicId,
    List<PostCreateResponseBaseBodyBlock> BodyBlocks
);
