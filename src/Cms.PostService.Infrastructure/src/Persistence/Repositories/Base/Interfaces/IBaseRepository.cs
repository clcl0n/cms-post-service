using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Domain.Entities;

namespace Cms.PostService.Infrastructure.Persistence.Repositories.Base.Interfaces;

public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsTrackingAsync(Guid id, CancellationToken cancellationToken);

    Task<TEntity?> GetByIdAsync(Guid id, bool withTracking = false, CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);

    Task InsertAsync(TEntity entity, CancellationToken cancellationToke);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToke);

    Task DeleteAsync(Guid id, CancellationToken cancellationToke);
}
