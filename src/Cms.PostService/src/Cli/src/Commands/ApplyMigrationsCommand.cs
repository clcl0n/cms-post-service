using System.Threading;
using System.Threading.Tasks;
using Cms.Cli.Commands.Interfaces;
using Cms.PostService.Infrastructure.Services.Interfaces;

namespace Cms.PostService.Cli.Commands;

public class ApplyMigrationsCommand(IPersistenceService persistenceService) : ICommand
{
    public Task ExecuteAsync(CancellationToken cancellationToken)
    {
        return persistenceService.ApplyMigrationsAsync(cancellationToken);
    }
}
