using System;

namespace Cms.PostService.Infrastructure.Contracts.Queries;

public sealed record TopicRouteByIdQueryResult(Guid Id, string FullPath);