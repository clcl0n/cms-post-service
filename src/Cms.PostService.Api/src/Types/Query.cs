using System;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Api.DataLoaders;
using HotChocolate.Types;

namespace Cms.PostService.Api.Types;

[QueryType]
public static class Query
{
    public static async Task<Topic?> GetTopicAsync(ITopicByIdDataLoader dataLoader, Guid id, CancellationToken cancellationToken)
    {
        var response = await dataLoader.LoadAsync(id, cancellationToken);

        return response is null ? null : new Topic(response.Id, response.Title);
    }

    public static async Task<SubTopic?> GetSubTopicAsync(ISubTopicByIdDataLoader dataLoader, Guid id, CancellationToken cancellationToken)
    {
        var response = await dataLoader.LoadAsync(id, cancellationToken);

        return response is null ? null : new SubTopic(response.Id, response.Title);
    }
}