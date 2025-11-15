using Cms.PostService.Application.Contracts.Commands;
using Cms.Shared.Handlers.Interfaces.Base;

namespace Cms.PostService.Application.Handlers.Commands.Interfaces;

public interface ITopicUpdateCommandHandler
    : IBaseHandler<TopicUpdateCommand, TopicUpdateCommandResponse?>;
