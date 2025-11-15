using System;
using System.Collections.Generic;
using Cms.PostService.Application.Contracts.Dtos;

namespace Cms.PostService.Application.Contracts.Queries;

public record PostGetByIdQueryResponse(
    Guid Id,
    string BodyPlainText,
    string Title,
    ImageDto ListingImage,
    TopicDto Topic,
    List<BaseBodyBlockDto> BodyBlocks
);
