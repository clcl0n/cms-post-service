using Cms.PostService.Application.Contracts.Queries;
using Cms.Shared.Handlers.Interfaces.Base;

namespace Cms.PostService.Application.Handlers.Commands.Interfaces;

public interface IPostSitemapDataCommandHandler
    : IBaseHandler<PostSitemapDataQuery, PostSitemapDataQueryResponse>;
