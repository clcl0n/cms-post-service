using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Infrastructure.Persistence;
using Cms.PostService.Infrastructure.Services.Interfaces;

namespace Cms.PostService.Infrastructure.Services;

internal sealed class PersistenceService(PostServiceDbContext dbContext) : IPersistenceService
{
    public Task ApplyMigrationsAsync(CancellationToken cancellationToken)
    {
        return dbContext.ApplyMigrations(cancellationToken);
    }
}
