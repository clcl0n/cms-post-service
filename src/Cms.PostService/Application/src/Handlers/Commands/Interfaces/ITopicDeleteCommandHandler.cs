using Cms.PostService.Application.Contracts.Commands.Topic;
using Cms.Shared.Handlers.Interfaces.Base;

namespace Cms.PostService.Application.Handlers.Commands.Interfaces;

public interface ITopicDeleteCommandHandler : IBaseHandler<TopicDeleteRequest>;
