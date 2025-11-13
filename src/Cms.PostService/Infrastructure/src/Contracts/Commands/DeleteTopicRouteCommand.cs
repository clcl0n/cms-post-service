using System;

namespace Cms.PostService.Infrastructure.Contracts.Commands;

public sealed record DeleteTopicRouteCommand(Guid Id);