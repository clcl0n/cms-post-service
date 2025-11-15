using System;

namespace Cms.PostService.Infrastructure.Contracts.Commands;

public sealed record DeletePostRouteCommand(Guid Id);