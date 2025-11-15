using Cms.PostService.Application.Contracts.Queries;
using Cms.Shared.Handlers.Interfaces.Base;

namespace Cms.PostService.Application.Handlers.Queries.Interfaces;

public interface ITopicGetByIdQueryHandler
    : IBaseHandler<TopicGetByIdQuery, TopicGetByIdQueryResponse?>;
