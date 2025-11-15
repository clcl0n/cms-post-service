using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Dtos;
using Cms.PostService.Application.Contracts.Queries;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace Cms.PostService.Application.Handlers.Commands;

internal sealed class PostSitemapDataCommandHandler(IUnitOfWork unitOfWork)
    : IPostSitemapDataCommandHandler
{
    public async Task<PostSitemapDataQueryResponse> HandleAsync(
        PostSitemapDataQuery request,
        CancellationToken cancellationToken
    )
    {
        var (data, totalCount) = await unitOfWork.PostRepository.GetSitemapsInfoAsync(
            request.Limit,
            request.Offset,
            cancellationToken
        );

        return new PostSitemapDataQueryResponse(
            request.Offset,
            request.Limit,
            totalCount,
            data.ConvertAll(x => new SitemapUrlDto(x.Id, x.Path, x.LastModified))
        );
    }
}
