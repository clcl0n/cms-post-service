namespace Cms.PostService.Application.Contracts.Queries.Post.GetById;

public record PostGetByIdResponseParagraphBodyBlock(int Order, string Content)
    : PostGetByIdResponseBaseBodyBlock(Order);
