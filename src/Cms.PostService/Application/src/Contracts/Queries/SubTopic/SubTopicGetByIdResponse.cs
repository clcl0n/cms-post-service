using System;

namespace Cms.PostService.Application.Contracts.Queries.SubTopic;

public record SubTopicGetByIdResponse(Guid Id, string Title);
