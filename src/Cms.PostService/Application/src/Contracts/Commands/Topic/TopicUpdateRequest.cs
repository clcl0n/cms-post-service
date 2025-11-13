using System;

namespace Cms.PostService.Application.Contracts.Commands.Topic;

public record TopicUpdateRequest(Guid Id, string Title);
