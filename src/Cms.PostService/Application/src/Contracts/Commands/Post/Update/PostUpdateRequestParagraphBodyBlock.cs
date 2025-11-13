namespace Cms.PostService.Application.Contracts.Commands.Post.Update;

public record PostUpdateRequestParagraphBodyBlock(int Order, string Content)
    : PostUpdateRequestBaseBodyBlock(Order);
