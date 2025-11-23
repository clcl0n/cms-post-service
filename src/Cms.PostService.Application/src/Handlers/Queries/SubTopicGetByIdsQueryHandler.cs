using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Dtos;
using Cms.PostService.Application.Contracts.Queries;
using Cms.PostService.Application.Handlers.Queries.Interfaces;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace Cms.PostService.Application.Handlers.Queries;

internal sealed class SubTopicGetByIdsQueryHandler(IUnitOfWork unitOfWork)
    : ISubTopicGetByIdsQueryHandler
{
    public async Task<SubTopicGetByIdsQueryResponse> HandleAsync(
        SubTopicGetByIdsQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await unitOfWork.SubTopicRepository.GetByIdsAsync(request.Ids, cancellationToken);

        return new SubTopicGetByIdsQueryResponse([.. result.Select(result => new SubTopicDto(result.Id, result.Title))]);
    }
}
