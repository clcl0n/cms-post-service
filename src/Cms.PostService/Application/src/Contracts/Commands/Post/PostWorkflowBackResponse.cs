using System;
using Cms.PostService.Domain.Constants;

namespace Cms.PostService.Application.Contracts.Commands.Post;

public record PostWorkflowBackResponse(Guid Id, PostStatus Status);
