using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Commands;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Cms.PostService.Infrastructure.Contracts.Commands;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;
using Cms.PostService.Infrastructure.Services.Interfaces;

namespace Cms.PostService.Application.Handlers.Commands;

internal sealed class PostDeleteCommandHandler(
    ISitemapService sitemapService,
    IUnitOfWork unitOfWork,
    IRouteService routeService
) : IPostDeleteCommandHandler
{
    public async Task HandleAsync(PostDeleteCommand request, CancellationToken cancellationToken)
    {
        var post = await unitOfWork.PostRepository.GetByIdAsync(request.Id, withTracking: true, cancellationToken);

        if (post == null)
        {
            return;
        }

        await unitOfWork.PostRepository.DeleteAsync(post, cancellationToken);

        await sitemapService.ScheduleDeleteUrlAsync(request.Id);

        foreach (var route in post.Routes)
        {
            await routeService.DeletePostRouteAsync(new DeletePostRouteCommand(route.Id), cancellationToken);
        }
    }
}
