using System;

namespace Cms.PostService.Application.Contracts.Queries;

public record TopicGetByIdQueryResponse(Guid Id, string Title);
