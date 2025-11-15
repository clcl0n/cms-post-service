using System;

namespace Cms.PostService.Application.Contracts.Commands;

public record PostDeleteCommand(Guid Id);
