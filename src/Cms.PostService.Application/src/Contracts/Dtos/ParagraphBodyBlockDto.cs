namespace Cms.PostService.Application.Contracts.Dtos;

public record ParagraphBodyBlockDto(int Order, string Content)
    : BaseBodyBlockDto(Order);
