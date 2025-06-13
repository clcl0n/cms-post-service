namespace Cms.PostService.Application.Contracts.Commands.Post.Create;

public record PostCreateRequestParagraphBodyBlock(int Order, string Content)
    : PostCreateRequestBaseBodyBlock(Order);
