using System;

namespace Cms.PostService.Api.Types;

public record CreateTopicInput(Guid ParentTopicId, string Title);