using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Domain.Entities;
using Cms.PostService.Infrastructure.Persistence.Repositories.Base;
using Cms.PostService.Infrastructure.Persistence.Repositories.Interfaces;
using Cms.PostService.Infrastructure.Persistence.Repositories.Projections;
using Microsoft.EntityFrameworkCore;

namespace Cms.PostService.Infrastructure.Persistence.Repositories;

internal sealed class PostRepository(DbContext dbContext)
    : BaseRepository<Post>(dbContext),
        IPostRepository
{
    public Task<bool> IsPublishedAsync(Guid id, CancellationToken cancellationToken)
    {
        return Entities
            .AsNoTracking()
            .AnyAsync(
                x => x.Id == id && x.Status == Domain.Constants.PostStatus.Published,
                cancellationToken
            );
    }

    public async Task<(List<PostSitemapInfoProjection> data, int totalCount)> GetSitemapsInfoAsync(
        int limit,
        int offset,
        CancellationToken cancellationToken
    )
    {
        var totalCount = await Entities.CountAsync(cancellationToken);

        var responseData = await Entities
            .AsNoTracking()
            .Select(x => new PostSitemapInfoProjection
            {
                Id = x.Id,
                Path = "path/test/", // TODO: add path
                LastModified = x.LastModified,
            })
            .Skip(offset)
            .Take(limit)
            .ToListAsync(cancellationToken);

        return (responseData, totalCount);
    }

    public Task<Post?> GetByIdForWorkflowAsTrackingAsync(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        return Entities.AsTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<Post?> GetByIdForUpdateAsTrackingAsync(Guid id, CancellationToken cancellationToken)
    {
        return Entities
            .AsTracking()
            .Include(x => x.Topic)
            .Include(x => x.BodyBlocks)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<PostWithDetailsProjection?> GetByIdWithDetailsAsync(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Topic)
            .Include(x => x.BodyBlocks)
            .Select(x => new PostWithDetailsProjection
            {
                Id = x.Id,
                Title = x.Title,
                BodyPlainText = x.BodyPlainText,
                ListingImageId = x.ListingImageId,
                Topic = new PostWithDetailsProjectionTopic
                {
                    Id = x.Topic!.Id,
                    Title = x.Topic.Title,
                },
                BodyBlocks = x.BodyBlocks.ToList(),
            })
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
