using System;
using Cms.Contracts.Constants;

namespace Cms.PostService.Api.Contracts.Dtos;

public record ImageDto(
    Guid Id,
    string FileName,
    long SizeInBytes,
    ImageFormat Format
);
