namespace Cms.PostService.Application.Contracts.Commands.Post.Update;

public record PostUpdateResponseParagraphBodyBlock(int Order, string Content)
    : PostUpdateResponseBaseBodyBlock(Order);
