using Cms.PostService.Application.Contracts.Commands.SubTopic;
using Cms.Shared.Handlers.Interfaces.Base;

namespace Cms.PostService.Application.Handlers.Commands.Interfaces;

public interface ISubTopicCreateCommandHandler
    : IBaseHandler<SubTopicCreateRequest, SubTopicCreateResponse>;
