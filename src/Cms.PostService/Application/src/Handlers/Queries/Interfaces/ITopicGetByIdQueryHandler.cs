using Cms.PostService.Application.Contracts.Queries.Topic;
using Cms.Shared.Handlers.Interfaces.Base;

namespace Cms.PostService.Application.Handlers.Queries.Interfaces;

public interface ITopicGetByIdQueryHandler
    : IBaseHandler<TopicGetByIdRequest, TopicGetByIdResponse?>;
