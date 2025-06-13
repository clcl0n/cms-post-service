using System;
using System.Threading.Tasks;

namespace Cms.PostService.Infrastructure.Services.Interfaces;

public interface ISitemapService
{
    ValueTask ScheduleDeleteUrlAsync(Guid id);

    ValueTask ScheduleUpsertUrlAsync(Guid id, string path, DateTime lastModified);
}
