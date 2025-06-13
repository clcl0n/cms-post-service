using System.Text.Json.Serialization;
using Cms.PostService.Domain.Constants;

namespace Cms.PostService.Application.Contracts.Commands.Post.Update;

[JsonDerivedType(
    typeof(PostUpdateResponseParagraphBodyBlock),
    typeDiscriminator: BodyBlockType.ParagraphBlock
)]
public record PostUpdateResponseBaseBodyBlock(int Order);
