using System.Threading;
using System.Threading.Tasks;

namespace Cms.PostService.Infrastructure.Services.Interfaces;

public interface IPersistenceService
{
    Task ApplyMigrationsAsync(CancellationToken cancellationToken);
}
