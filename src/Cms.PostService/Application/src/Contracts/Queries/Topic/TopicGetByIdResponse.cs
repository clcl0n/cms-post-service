using System;

namespace Cms.PostService.Application.Contracts.Queries.Topic;

public record TopicGetByIdResponse(Guid Id, string Title);
