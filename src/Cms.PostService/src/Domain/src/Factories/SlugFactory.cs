using System;
using Slugify;

namespace Cms.PostService.Domain.Factories;

public static class SlugFactory
{
    public static string Create(string title)
    {
        ArgumentException.ThrowIfNullOrEmpty(title, nameof(title));

        SlugHelper helper = new();

        return helper.GenerateSlug(title);
    }
}
