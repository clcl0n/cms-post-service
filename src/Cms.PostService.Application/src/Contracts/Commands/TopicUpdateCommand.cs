using System;

namespace Cms.PostService.Application.Contracts.Commands;

public record TopicUpdateCommand(Guid Id, string Title);
