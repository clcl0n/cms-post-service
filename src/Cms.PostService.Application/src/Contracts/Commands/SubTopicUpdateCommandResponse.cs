using System;

namespace Cms.PostService.Application.Contracts.Commands;

public record SubTopicUpdateCommandResponse(Guid Id, string Title);
