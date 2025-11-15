using System;
using Cms.PostService.Domain.Constants;

namespace Cms.PostService.Application.Contracts.Commands;

public record PostWorkflowNextCommandResponse(Guid Id, PostStatus Status);
