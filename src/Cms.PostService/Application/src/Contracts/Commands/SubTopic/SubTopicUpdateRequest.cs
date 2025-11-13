using System;

namespace Cms.PostService.Application.Contracts.Commands.SubTopic;

public record SubTopicUpdateRequest(Guid Id, string Title);
