using System;

namespace Cms.PostService.Application.Contracts.Commands.Topic;

public record TopicUpdateResponse(Guid Id, string Title);
