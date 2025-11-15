using System;

namespace Cms.PostService.Application.Contracts.Commands;

public record TopicCreateCommandResponse(Guid Id, string Title);
