using System.Text.Json.Serialization;
using Cms.PostService.Domain.Constants;

namespace Cms.PostService.Api.Contracts.Dtos;

[JsonDerivedType(
    typeof(ParagraphBodyBlockDto),
    typeDiscriminator: BodyBlockType.ParagraphBlock
)]
public record BaseBodyBlockDto(int Order);
