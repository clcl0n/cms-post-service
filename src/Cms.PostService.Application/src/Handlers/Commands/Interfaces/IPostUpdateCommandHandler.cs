using Cms.PostService.Application.Contracts.Commands;
using Cms.Shared.Handlers.Interfaces.Base;

namespace Cms.PostService.Application.Handlers.Commands.Interfaces;

public interface IPostUpdateCommandHandler : IBaseHandler<PostUpdateCommand, PostUpdateCommandResponse?>;
