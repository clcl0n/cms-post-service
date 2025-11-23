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

internal static class SubTopicDataLoader
{
    [DataLoader]
    public static async Task<Dictionary<Guid, SubTopic>> GetSubTopicByIdAsync(
        IReadOnlyList<Guid> ids,
        ISubTopicGetByIdsQueryHandler queryHandler,
        CancellationToken cancellationToken)
    {
        var response = await queryHandler.HandleAsync(new SubTopicGetByIdsQuery([.. ids]), cancellationToken);

        // TODO: mapping
        return response.SubTopic.ToDictionary(
            subtopic => subtopic.Id,
            subtopic => new SubTopic(subtopic.Id, subtopic.Title));
    }
}