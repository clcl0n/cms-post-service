using System;
using Cms.PostService.Domain.Constants;

namespace Cms.PostService.Domain.Events;

public record PostWorkflowEvent(Guid PostId, PostStatus PreviousStatus, PostStatus CurrentStatus);
