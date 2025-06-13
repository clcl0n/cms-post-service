using System.Text.Json.Serialization;
using Cms.PostService.Domain.Constants;

namespace Cms.PostService.Application.Contracts.Commands.Post.Update;

[JsonDerivedType(
    typeof(PostUpdateRequestParagraphBodyBlock),
    typeDiscriminator: BodyBlockType.ParagraphBlock
)]
public record PostUpdateRequestBaseBodyBlock(int Order);
