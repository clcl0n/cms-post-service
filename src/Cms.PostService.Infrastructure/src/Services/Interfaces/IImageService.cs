using System;
using System.Threading;
using System.Threading.Tasks;
using Cms.Contracts;

namespace Cms.PostService.Infrastructure.Services.Interfaces;

public interface IImageService
{
    Task<ImageGetByIdResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
