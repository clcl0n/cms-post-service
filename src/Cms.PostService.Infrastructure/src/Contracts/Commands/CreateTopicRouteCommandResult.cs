using System;

namespace Cms.PostService.Infrastructure.Contracts.Commands;

public sealed record CreateTopicRouteCommandResult(Guid Id, string FullPath);