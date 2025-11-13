namespace Cms.PostService.Application.Contracts.Commands.Post.Create;

public record PostCreateResponseParagraphBodyBlock(int Order, string Content)
    : PostCreateResponseBaseBodyBlock(Order);
