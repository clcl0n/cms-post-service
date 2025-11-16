using System;
using System.Collections.Generic;
using Cms.PostService.Api.Contracts.Dtos;

namespace Cms.PostService.Api.Contracts.Responses;

public record PostGetByIdResponse(
    Guid Id,
    string BodyPlainText,
    string Title,
    ImageDto ListingImage,
    TopicDto Topic,
    List<BaseBodyBlockDto> BodyBlocks
);
