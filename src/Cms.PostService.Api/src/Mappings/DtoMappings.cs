using System;
using Cms.PostService.Api.Contracts.Dtos;

namespace Cms.PostService.Api.Mappings;

internal static class DtoMappings
{
    public static ImageDto ToImageDto(Application.Contracts.Dtos.ImageDto dto)
    {
        return new(
            dto.Id,
            dto.FileName,
            dto.SizeInBytes,
            dto.Format
        );
    }

    public static TopicDto ToTopicDto(Application.Contracts.Dtos.TopicDto dto)
    {
        return new(
            dto.Id,
            dto.Title
        );
    }

    public static BaseBodyBlockDto ToBodyBlockDto(Application.Contracts.Dtos.BaseBodyBlockDto request)
    {
        return request switch
        {
            Application.Contracts.Dtos.ParagraphBodyBlockDto paragraphBlock => new ParagraphBodyBlockDto(paragraphBlock.Order, paragraphBlock.Content),
            _ => throw new NotImplementedException(),
        };
    }
}