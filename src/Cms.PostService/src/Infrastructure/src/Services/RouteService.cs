using System;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Infrastructure.Contracts.Commands;
using Cms.PostService.Infrastructure.Contracts.Queries;
using Cms.PostService.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;

using static Cms.Protos.PostRouteService;
using static Cms.Protos.TopicRouteService;

namespace Cms.PostService.Infrastructure.Services;

internal sealed class RouteService(
    PostRouteServiceClient postRouteClient,
    TopicRouteServiceClient topicRouteClient,
    ILogger<IRouteService> logger) : IRouteService
{
    public async Task<CreateTopicRouteCommandResult> CreateTopicRouteAsync(
        CreateTopicRouteCommand command,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentException.ThrowIfNullOrEmpty(command.Slug);

        var response = await ExecuteGrpcCallAsync(
            async () => await topicRouteClient.CreateAsync(new Protos.TopicRouteCreateRequest { Slug = command.Slug }, cancellationToken: cancellationToken),
            $"Create topic route for slug '{command.Slug}'"
        );

        return new CreateTopicRouteCommandResult(ParseGuid(response.Id), response.FullPath);
    }

    public async Task<CreatePostRouteCommandResult> CreatePostRouteAsync(
        CreatePostRouteCommand command,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentException.ThrowIfNullOrEmpty(command.Slug);

        var response = await ExecuteGrpcCallAsync(
            async () => await postRouteClient.CreateAsync(new Protos.PostRouteCreateRequest { Slug = command.Slug }, cancellationToken: cancellationToken),
            $"Create post route for slug '{command.Slug}'"
        );

        return new CreatePostRouteCommandResult(ParseGuid(response.Id), response.FullPath);
    }

    public async Task<TopicRouteByIdQueryResult> GetTopicRouteByIdAsync(
        TopicRouteByIdQuery query,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(query);

        var response = await ExecuteGrpcCallAsync(
            async () => await topicRouteClient.GetByIdAsync(new Protos.TopicRouteByIdRequest { Id = query.Id.ToString() }, cancellationToken: cancellationToken),
            $"Get topic route by id '{query.Id}'"
        );

        return new TopicRouteByIdQueryResult(ParseGuid(response.Id), response.FullPath);
    }

    public async Task<PostRouteByIdQueryResult> GetPostRouteByIdAsync(
        PostRouteByIdQuery query,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(query);

        var response = await ExecuteGrpcCallAsync(
            async () => await postRouteClient.GetByIdAsync(new Protos.PostRouteByIdRequest { Id = query.Id.ToString() }, cancellationToken: cancellationToken),
            $"Get post route by id '{query.Id}'"
        );

        return new PostRouteByIdQueryResult(ParseGuid(response.Id), response.FullPath);
    }

    public async Task DeleteTopicRouteAsync(DeleteTopicRouteCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        await ExecuteGrpcCallAsync(
            async () => await topicRouteClient.DeleteAsync(new Protos.TopicRouteDeleteRequest { Id = command.Id.ToString() }, cancellationToken: cancellationToken),
            $"Delete topic route by id '{command.Id}'"
        );
    }

    public async Task DeletePostRouteAsync(DeletePostRouteCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        await ExecuteGrpcCallAsync(
            async () => await postRouteClient.DeleteAsync(new Protos.PostRouteDeleteRequest { Id = command.Id.ToString() }, cancellationToken: cancellationToken),
            $"Delete post route by id '{command.Id}'"
        );
    }

    private async Task<T> ExecuteGrpcCallAsync<T>(
        Func<Task<T>> operation,
        string operationName
    )
    {
        try
        {
            var result = await operation();

            logger.LogDebug("Successfully completed {Operation}", operationName);

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to {Operation}", operationName);

            throw;
        }
    }

    private static Guid ParseGuid(string idString)
    {
        if (!Guid.TryParse(idString, out var id))
        {
            throw new InvalidOperationException($"Id string '{idString}' is not a valid GUID.");
        }

        return id;
    }
}
