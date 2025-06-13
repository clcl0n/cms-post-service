using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Queries.SubTopic;
using Cms.PostService.Application.Handlers.Queries.Interfaces;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace Cms.PostService.Application.Handlers.Queries;

internal sealed class SubTopicGetByIdQueryHandler(IUnitOfWork unitOfWork)
    : ISubTopicGetByIdQueryHandler
{
    public async Task<SubTopicGetByIdResponse?> HandleAsync(
        SubTopicGetByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await unitOfWork.SubTopicRepository.GetByIdAsync(
            request.Id,
            cancellationToken
        );

        if (result is null)
        {
            return null;
        }

        return new SubTopicGetByIdResponse(result.Id, result.Title);
    }
}
