using System;
using System.Collections.Generic;

namespace Cms.PostService.Application.Contracts.Queries;

public record SubTopicGetByIdsQuery(List<Guid> Ids);
