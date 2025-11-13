using Cms.PostService.Application.Contracts.Queries.SubTopic;
using Cms.Shared.Handlers.Interfaces.Base;

namespace Cms.PostService.Application.Handlers.Queries.Interfaces;

public interface ISubTopicGetByIdQueryHandler
    : IBaseHandler<SubTopicGetByIdRequest, SubTopicGetByIdResponse?>;
