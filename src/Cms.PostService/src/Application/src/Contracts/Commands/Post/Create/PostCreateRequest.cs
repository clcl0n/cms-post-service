using System;
using System.Collections.Generic;

namespace Cms.PostService.Application.Contracts.Commands.Post.Create;

public record PostCreateRequest(
    string Title,
    Guid ListingImageId,
    Guid TopicId,
    List<PostCreateRequestBaseBodyBlock> BodyBlocks
);
