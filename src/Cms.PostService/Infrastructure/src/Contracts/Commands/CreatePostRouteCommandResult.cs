using System;

namespace Cms.PostService.Infrastructure.Contracts.Commands;

public sealed record CreatePostRouteCommandResult(Guid Id, string FullPath);