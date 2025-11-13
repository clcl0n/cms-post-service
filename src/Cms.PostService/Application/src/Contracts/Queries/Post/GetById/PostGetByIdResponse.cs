using System;
using System.Collections.Generic;

namespace Cms.PostService.Application.Contracts.Queries.Post.GetById;

public record PostGetByIdResponse(
    Guid Id,
    string BodyPlainText,
    string Title,
    PostGetByIdResponseImage ListingImage,
    PostGetByIdResponseTopic Topic,
    List<PostGetByIdResponseBaseBodyBlock> BodyBlocks
);
