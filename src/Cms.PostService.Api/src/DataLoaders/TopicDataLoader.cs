using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Api.Types;
using Cms.PostService.Application.Contracts.Queries;
using Cms.PostService.Application.Handlers.Queries.Interfaces;
using GreenDonut;

namespace Cms.PostService.Api.DataLoaders;

internal static class TopicDataLoader
{
    [DataLoader]
    public static async Task<Dictionary<Guid, Topic>> GetTopicByIdAsync(
        IReadOnlyList<Guid> ids,
        ITopicGetByIdsQueryHandler queryHandler,
        CancellationToken cancellationToken)
    {
        var response = await queryHandler.HandleAsync(new TopicGetByIdsQuery([.. ids]), cancellationToken);

        // TODO: mapping
        return response.Topics.ToDictionary(
            topic => topic.Id,
            topic => new Topic(topic.Id, topic.Title));
    }
}