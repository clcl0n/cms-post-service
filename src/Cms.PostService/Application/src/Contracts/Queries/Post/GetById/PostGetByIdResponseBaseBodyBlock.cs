using System.Text.Json.Serialization;
using Cms.PostService.Domain.Constants;

namespace Cms.PostService.Application.Contracts.Queries.Post.GetById;

[JsonDerivedType(
    typeof(PostGetByIdResponseParagraphBodyBlock),
    typeDiscriminator: BodyBlockType.ParagraphBlock
)]
public record PostGetByIdResponseBaseBodyBlock(int Order);
