using System;

namespace Cms.PostService.Application.Contracts.Commands;

public record SubTopicCreateCommandResponse(Guid Id, string Title);
