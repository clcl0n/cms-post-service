using System;

namespace Cms.PostService.Api.Contracts.Requests;

public record SubTopicCreateRequest(Guid ParentTopicId, string Title);