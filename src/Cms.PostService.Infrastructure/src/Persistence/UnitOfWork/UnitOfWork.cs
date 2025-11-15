using System;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Infrastructure.Persistence.Repositories;
using Cms.PostService.Infrastructure.Persistence.Repositories.Interfaces;
using Cms.PostService.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace Cms.PostService.Infrastructure.Persistence.UnitOfWork;

internal sealed class UnitOfWork(PostServiceDbContext dbContext) : IUnitOfWork
{
    public IPostRepository PostRepository => _lazyPostRepository.Value;

    public ITopicRepository TopicRepository => _lazyTopicRepository.Value;

    public ISubTopicRepository SubTopicRepository => _lazySubTopicRepository.Value;

    private readonly Lazy<IPostRepository> _lazyPostRepository = new(
        () => new PostRepository(dbContext)
    );

    private readonly Lazy<ITopicRepository> _lazyTopicRepository = new(
        () => new TopicRepository(dbContext)
    );

    private readonly Lazy<ISubTopicRepository> _lazySubTopicRepository = new(
        () => new SubTopicRepository(dbContext)
    );

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
