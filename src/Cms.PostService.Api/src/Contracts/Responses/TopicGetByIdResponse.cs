using System;

namespace Cms.PostService.Api.Contracts.Responses;

public record TopicGetByIdResponse(Guid Id, string Title);
