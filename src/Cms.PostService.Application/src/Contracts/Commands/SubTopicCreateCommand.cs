using System;

namespace Cms.PostService.Application.Contracts.Commands;

public record SubTopicCreateCommand(Guid ParentTopicId, string Title);
