using System;

namespace Cms.PostService.Application.Contracts.Commands.SubTopic;

public record SubTopicUpdateResponse(Guid Id, string Title);
