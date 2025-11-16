using System;
using Cms.PostService.Domain.Constants;

namespace Cms.PostService.Api.Contracts.Responses;

public record PostWorkflowNextResponse(Guid Id, PostStatus Status);
