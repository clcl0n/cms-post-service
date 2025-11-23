using System;

namespace Cms.PostService.Api.Types;

public record UpdateTopicInput(Guid Id, string Title);