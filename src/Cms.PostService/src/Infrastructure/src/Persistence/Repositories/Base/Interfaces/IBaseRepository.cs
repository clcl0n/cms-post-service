using System;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Domain.Entities;

namespace Cms.PostService.Infrastructure.Persistence.Repositories.Base.Interfaces;

public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsTrackingAsync(Guid id, CancellationToken cancellationToke);

    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToke);

    Task InsertAsync(TEntity entity, CancellationToken cancellationToke);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToke);

    Task DeleteAsync(Guid id, CancellationToken cancellationToke);
}
