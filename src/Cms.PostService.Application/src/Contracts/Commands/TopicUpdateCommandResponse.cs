using System;

namespace Cms.PostService.Application.Contracts.Commands;

public record TopicUpdateCommandResponse(Guid Id, string Title);
