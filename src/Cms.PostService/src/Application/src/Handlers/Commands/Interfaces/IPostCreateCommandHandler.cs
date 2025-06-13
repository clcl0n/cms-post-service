using Cms.PostService.Application.Contracts.Commands.Post.Create;
using Cms.Shared.Handlers.Interfaces.Base;

namespace Cms.PostService.Application.Handlers.Commands.Interfaces;

public interface IPostCreateCommandHandler : IBaseHandler<PostCreateRequest, PostCreateResponse>;
