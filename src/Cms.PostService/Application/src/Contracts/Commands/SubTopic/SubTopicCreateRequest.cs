using System;

namespace Cms.PostService.Application.Contracts.Commands.SubTopic;

public record SubTopicCreateRequest(Guid ParentTopicId, string Title);
