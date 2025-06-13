using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Infrastructure.Persistence.Repositories.Interfaces;

namespace Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;

public interface IUnitOfWork
{
    IPostRepository PostRepository { get; }

    ITopicRepository TopicRepository { get; }

    ISubTopicRepository SubTopicRepository { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
