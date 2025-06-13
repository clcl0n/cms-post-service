using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Commands.Post;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace Cms.PostService.Application.Handlers.Commands;

internal sealed class PostBulkSitemapDataCommandHandler(IUnitOfWork unitOfWork)
    : IPostBulkSitemapDataCommandHandler
{
    public async Task<PostBulkSitemapDataResponse> HandleAsync(
        PostBulkSitemapDataRequest request,
        CancellationToken cancellationToken
    )
    {
        var (data, totalCount) = await unitOfWork.PostRepository.GetSitemapsInfoAsync(
            request.Limit,
            request.Offset,
            cancellationToken
        );

        return new PostBulkSitemapDataResponse(
            request.Offset,
            request.Limit,
            totalCount,
            data.ConvertAll(x => new PostBulkSitemapDataResponseUrl(x.Id, x.Path, x.LastModified))
        );
    }
}
