using Cms.PostService.Application.Contracts.Queries.Post.GetById;
using Cms.Shared.Handlers.Interfaces.Base;

namespace Cms.PostService.Application.Handlers.Queries.Interfaces;

public interface IPostGetPaginationQueryHandler
    : IBaseHandler<PostGetPaginationRequest, PostGetPaginationResponse>;
