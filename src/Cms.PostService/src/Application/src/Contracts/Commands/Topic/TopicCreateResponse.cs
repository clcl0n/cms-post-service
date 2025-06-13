using System;

namespace Cms.PostService.Application.Contracts.Commands.Topic;

public record TopicCreateResponse(Guid Id, string Title);
