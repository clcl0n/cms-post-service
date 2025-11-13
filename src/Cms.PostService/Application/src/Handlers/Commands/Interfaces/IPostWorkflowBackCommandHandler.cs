using Cms.PostService.Application.Contracts.Commands.Post;
using Cms.Shared.Handlers.Interfaces.Base;

namespace Cms.PostService.Application.Handlers.Commands.Interfaces;

public interface IPostWorkflowBackCommandHandler
    : IBaseHandler<PostWorkflowBackRequest, PostWorkflowBackResponse?>;
