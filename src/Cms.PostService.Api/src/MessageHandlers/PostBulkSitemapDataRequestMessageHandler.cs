using System.Threading;
using System.Threading.Tasks;
using Cms.Contracts;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Wolverine.Attributes;

namespace Cms.PostService.Api.MessageHandlers;

// TODO: grpc
[WolverineHandler]
public sealed class PostBulkSitemapDataRequestMessageHandler(
    IPostSitemapDataCommandHandler postSitemapDataRequestHandler
)
{
    public async Task<BulkSitemapDataResponse> HandleAsync(
        PostBulkSitemapDataRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await postSitemapDataRequestHandler.HandleAsync(
            new Application.Contracts.Queries.PostSitemapDataQuery(
                request.Limit,
                request.Offset
            ),
            cancellationToken
        );

        return new BulkSitemapDataResponse(
            response.Offset,
            response.Limit,
            response.TotalCount,
            response.Urls.ConvertAll(x => new BulkSitemapDataResponseUrl(
                x.EntityId,
                x.Path,
                x.LastModified
            ))
        );
    }
}
