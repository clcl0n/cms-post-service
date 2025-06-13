using System;
using System.Text;
using Cms.PostService.Domain.Entities.BodyBlocks;

namespace Cms.PostService.Domain.Builders;

public sealed class BodyPlainTextBuilder
{
    private readonly StringBuilder _bodyTextBuilder = new();

    public void Append(BaseBodyBlock bodyBlock)
    {
        switch (bodyBlock)
        {
            case ParagraphBodyBlock paragraphBodyBlock:
                Append(paragraphBodyBlock);
                break;
            default:
                throw new NotImplementedException(
                    $"Body block of type {bodyBlock.GetType().Name} is not supported."
                );
        }
    }

    public void Append(ParagraphBodyBlock paragraphBodyBlock)
    {
        _bodyTextBuilder.Append(paragraphBodyBlock.Content);
    }

    public string GetResult()
    {
        return _bodyTextBuilder.ToString();
    }

    public void Clear()
    {
        _bodyTextBuilder.Clear();
    }
}
