using System;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Domain.Entities;
using Cms.PostService.Infrastructure.Persistence.Repositories.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cms.PostService.Infrastructure.Persistence.Repositories.Base;

internal abstract class BaseRepository<TEntity>(DbContext dbContext) : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected DbSet<TEntity> Entities { get; } = dbContext.Set<TEntity>();

    protected DbContext DbContext { get; } = dbContext;

    public Task<TEntity?> GetByIdAsTrackingAsync(Guid id, CancellationToken cancellationToken)
    {
        return Entities.AsTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Entities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Entities.AddAsync(entity, cancellationToken);

        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Entities.Remove(entity);

        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await Entities.FindAsync([id], cancellationToken);

        if (entity == null)
        {
            return;
        }

        Entities.Remove(entity);

        await DbContext.SaveChangesAsync(cancellationToken);
    }
}
