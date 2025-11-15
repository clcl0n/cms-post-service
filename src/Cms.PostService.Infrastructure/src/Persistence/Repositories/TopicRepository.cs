using Cms.PostService.Domain.Entities;
using Cms.PostService.Infrastructure.Persistence.Repositories.Base;
using Cms.PostService.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cms.PostService.Infrastructure.Persistence.Repositories;

internal sealed class TopicRepository(DbContext dbContext)
    : BaseRepository<Topic>(dbContext),
        ITopicRepository;
