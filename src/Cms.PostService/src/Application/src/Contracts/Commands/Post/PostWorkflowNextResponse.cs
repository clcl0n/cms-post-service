using System;
using Cms.PostService.Domain.Constants;

namespace Cms.PostService.Application.Contracts.Commands.Post;

public record PostWorkflowNextResponse(Guid Id, PostStatus Status);
