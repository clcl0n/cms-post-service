using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Domain.Entities;
using Cms.PostService.Infrastructure.Persistence.Repositories.Base.Interfaces;
using Cms.PostService.Infrastructure.Persistence.Repositories.Projections;

namespace Cms.PostService.Infrastructure.Persistence.Repositories.Interfaces;

public interface IPostRepository : IBaseRepository<Post>
{
    Task<bool> IsPublishedAsync(Guid id, CancellationToken cancellationToken);

    Task<(List<PostSitemapInfoProjection> data, int totalCount)> GetSitemapsInfoAsync(
        int limit,
        int offset,
        CancellationToken cancellationToken
    );

    Task<Post?> GetByIdForWorkflowAsTrackingAsync(Guid id, CancellationToken cancellationToken);

    Task<Post?> GetByIdForUpdateAsTrackingAsync(Guid id, CancellationToken cancellationToken);

    Task<PostWithDetailsProjection?> GetByIdWithDetailsAsync(
        Guid id,
        CancellationToken cancellationToke
    );
}
