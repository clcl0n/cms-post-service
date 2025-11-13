using Cms.PostService.Application.Contracts.Commands.Post.Update;
using Cms.Shared.Handlers.Interfaces.Base;

namespace Cms.PostService.Application.Handlers.Commands.Interfaces;

public interface IPostUpdateCommandHandler : IBaseHandler<PostUpdateRequest, PostUpdateResponse?>;
