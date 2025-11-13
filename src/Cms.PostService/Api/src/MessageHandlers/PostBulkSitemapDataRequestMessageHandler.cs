using System.Threading;
using System.Threading.Tasks;
using Cms.Contracts;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Wolverine.Attributes;

namespace Cms.PostService.Api.MessageHandlers;

[WolverineHandler]
public sealed class PostBulkSitemapDataRequestMessageHandler(
    IPostBulkSitemapDataCommandHandler postBulkSitemapDataRequestHandler
)
{
    public async Task<BulkSitemapDataResponse> HandleAsync(
        PostBulkSitemapDataRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await postBulkSitemapDataRequestHandler.HandleAsync(
            new Application.Contracts.Commands.Post.PostBulkSitemapDataRequest(
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
