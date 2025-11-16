namespace Cms.PostService.Api.Contracts.Dtos;

public record ParagraphBodyBlockDto(int Order, string Content)
    : BaseBodyBlockDto(Order);
