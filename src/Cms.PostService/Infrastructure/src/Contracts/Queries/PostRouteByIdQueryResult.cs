using System;

namespace Cms.PostService.Infrastructure.Contracts.Queries;

public sealed record PostRouteByIdQueryResult(Guid Id, string FullPath);