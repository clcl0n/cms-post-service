using System;

namespace Cms.PostService.Application.Contracts.Commands;

public record SubTopicUpdateCommand(Guid Id, string Title);
