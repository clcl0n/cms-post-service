using System;
using Cms.Contracts.Constants;

namespace Cms.PostService.Application.Contracts.Queries.Post.GetById;

public record PostGetByIdResponseImage(
    Guid Id,
    string FileName,
    long SizeInBytes,
    ImageFormat Format
);
