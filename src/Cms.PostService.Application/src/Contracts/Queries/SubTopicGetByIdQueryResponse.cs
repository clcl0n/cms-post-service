using System;

namespace Cms.PostService.Application.Contracts.Queries;

public record SubTopicGetByIdQueryResponse(Guid Id, string Title);
