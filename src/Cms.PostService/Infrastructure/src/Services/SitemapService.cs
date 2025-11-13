using System;
using System.Threading.Tasks;
using Cms.Contracts;
using Cms.PostService.Infrastructure.Services.Interfaces;
using Wolverine;

namespace Cms.PostService.Infrastructure.Services;

internal sealed class SitemapService(IMessageBus bus) : ISitemapService
{
    public ValueTask ScheduleDeleteUrlAsync(Guid id)
    {
        return bus.PublishAsync(new PostSitemapDeleteUrlRequest(id));
    }

    public ValueTask ScheduleUpsertUrlAsync(Guid id, string path, DateTime lastModified)
    {
        return bus.PublishAsync(new PostSitemapUpsertUrlRequest(id, path, lastModified));
    }
}
