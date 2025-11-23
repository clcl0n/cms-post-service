using System;
using System.Collections.Generic;
using Cms.PostService.Application.Contracts.Dtos;

namespace Cms.PostService.Application.Contracts.Queries;

public record SubTopicGetByIdsQueryResponse(List<SubTopicDto> SubTopic);
