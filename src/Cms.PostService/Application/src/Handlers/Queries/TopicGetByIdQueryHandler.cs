using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Queries.Topic;
using Cms.PostService.Application.Handlers.Queries.Interfaces;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace Cms.PostService.Application.Handlers.Queries;

internal sealed class TopicGetByIdQueryHandler(IUnitOfWork unitOfWork) : ITopicGetByIdQueryHandler
{
    public async Task<TopicGetByIdResponse?> HandleAsync(
        TopicGetByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await unitOfWork.TopicRepository.GetByIdAsync(request.Id, cancellationToken);

        if (result is null)
        {
            return null;
        }

        return new TopicGetByIdResponse(result.Id, result.Title);
    }
}
