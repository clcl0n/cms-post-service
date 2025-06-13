using System.Text.Json.Serialization;
using Cms.PostService.Domain.Constants;

namespace Cms.PostService.Application.Contracts.Commands.Post.Create;

[JsonDerivedType(
    typeof(PostCreateRequestParagraphBodyBlock),
    typeDiscriminator: BodyBlockType.ParagraphBlock
)]
public record PostCreateRequestBaseBodyBlock(int Order);
