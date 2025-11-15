using System;
using Cms.PostService.Domain.Constants;

namespace Cms.PostService.Application.Contracts.Commands;

public record PostWorkflowBackCommandResponse(Guid Id, PostStatus Status);
