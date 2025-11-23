using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Commands;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using HotChocolate.Types;

namespace Cms.PostService.Api.Types;

[MutationType]
public static class Mutation
{
    public static async Task<Topic?> AddTopicAsync(ITopicCreateCommandHandler commandHandler, CreateTopicInput input, CancellationToken cancellationToken)
    {
        var response = await commandHandler.HandleAsync(new TopicCreateCommand(input.Title), cancellationToken);

        return new Topic(response.Id, response.Title);
    }

    public static async Task<Topic?> UpdateTopicAsync(ITopicUpdateCommandHandler commandHandler, UpdateTopicInput input, CancellationToken cancellationToken)
    {
        var response = await commandHandler.HandleAsync(new TopicUpdateCommand(input.Id, input.Title), cancellationToken);

        return response is null ? null : new Topic(response.Id, response.Title);
    }

    public static async Task<bool> DeleteTopicAsync(ITopicDeleteCommandHandler commandHandler, DeleteTopicInput input, CancellationToken cancellationToken)
    {
        return await commandHandler.HandleAsync(new TopicDeleteCommand(input.Id), cancellationToken);
    }

    public static async Task<Topic?> AddSubTopicAsync(ISubTopicCreateCommandHandler commandHandler, CreateTopicInput input, CancellationToken cancellationToken)
    {
        var response = await commandHandler.HandleAsync(new SubTopicCreateCommand(input.ParentTopicId, input.Title), cancellationToken);

        return new Topic(response.Id, response.Title);
    }

    public static async Task<Topic?> UpdateSubTopicAsync(ISubTopicUpdateCommandHandler commandHandler, UpdateTopicInput input, CancellationToken cancellationToken)
    {
        var response = await commandHandler.HandleAsync(new SubTopicUpdateCommand(input.Id, input.Title), cancellationToken);

        return response is null ? null : new Topic(response.Id, response.Title);
    }

    public static async Task<bool> DeleteSubTopicAsync(ISubTopicDeleteCommandHandler commandHandler, DeleteTopicInput input, CancellationToken cancellationToken)
    {
        return await commandHandler.HandleAsync(new SubTopicDeleteCommand(input.Id), cancellationToken);
    }
}