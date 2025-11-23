using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Dtos;
using Cms.PostService.Application.Contracts.Queries;
using Cms.PostService.Application.Handlers.Queries.Interfaces;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace Cms.PostService.Application.Handlers.Queries;

internal sealed class TopicGetByIdsQueryHandler(IUnitOfWork unitOfWork) : ITopicGetByIdsQueryHandler
{
    public async Task<TopicGetByIdsQueryResponse> HandleAsync(
        TopicGetByIdsQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await unitOfWork.TopicRepository.GetByIdsAsync(request.Ids, cancellationToken);

        return new TopicGetByIdsQueryResponse([.. result.Select(topic => new TopicDto(topic.Id, topic.Title))]);
    }
}
