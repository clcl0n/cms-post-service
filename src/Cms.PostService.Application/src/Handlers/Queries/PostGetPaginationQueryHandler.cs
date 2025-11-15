using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Queries;
using Cms.PostService.Application.Handlers.Queries.Interfaces;
using Cms.PostService.Infrastructure.Persistence.Repositories.Projections;
using Cms.PostService.Infrastructure.Persistence.Repositories.Queries;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace Cms.PostService.Application.Handlers.Queries;

internal sealed class PostGetPaginationQueryHandler(IUnitOfWork unitOfWork)
    : IPostGetPaginationQueryHandler
{
    public async Task<PostGetPaginationQueryResponse> HandleAsync(
        PostGetPaginationQuery request,
        CancellationToken cancellationToken
    )
    {
        var page = request.Page;
        var pageSize = request.PageSize;

        var (items, totalCount) = await unitOfWork.PostRepository.GetPaginationAsync(
            new PostPaginationQuery(Page: page, PageSize: pageSize),
            cancellationToken
        );

        return ToPostGetPaginationResponse(page, pageSize, totalCount, items);
    }

    private static PostGetPaginationQueryResponse ToPostGetPaginationResponse(
        int page,
        int pageSize,
        int totalCount,
        IEnumerable<PostPaginationProjection> items
    )
    {
        return new PostGetPaginationQueryResponse(
            Page: page,
            PageSize: pageSize,
            TotalCount: totalCount,
            Items: [.. items.Select(item => new PostGetPaginationQueryResponseItem(
                Id: item.Id,
                Title: item.Title,
                Status: item.Status,
                TopicTitle: item.TopicTitle
            ))]
        );
    }
}
