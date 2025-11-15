using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Queries;
using Cms.PostService.Application.Handlers.Queries.Interfaces;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace Cms.PostService.Application.Handlers.Queries;

internal sealed class TopicGetByIdQueryHandler(IUnitOfWork unitOfWork) : ITopicGetByIdQueryHandler
{
    public async Task<TopicGetByIdQueryResponse?> HandleAsync(
        TopicGetByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await unitOfWork.TopicRepository.GetByIdAsync(request.Id, cancellationToken);

        if (result is null)
        {
            return null;
        }

        return new TopicGetByIdQueryResponse(result.Id, result.Title);
    }
}
