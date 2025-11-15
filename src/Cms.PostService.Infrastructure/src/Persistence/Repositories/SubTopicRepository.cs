using Cms.PostService.Domain.Entities;
using Cms.PostService.Infrastructure.Persistence.Repositories.Base;
using Cms.PostService.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cms.PostService.Infrastructure.Persistence.Repositories;

internal sealed class SubTopicRepository(DbContext dbContext)
    : BaseRepository<SubTopic>(dbContext),
        ISubTopicRepository;
