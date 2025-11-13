using Cms.PostService.Domain.Entities.BodyBlocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.PostService.Infrastructure.Persistence.Configurations.BodyBlocks;

internal class ParagraphBodyBlockConfiguration : IEntityTypeConfiguration<ParagraphBodyBlock>
{
    public void Configure(EntityTypeBuilder<ParagraphBodyBlock> builder)
    {
        builder.ToTable("paragraph_body_block");

        builder.Property(x => x.Content);

        builder
            .HasOne<BaseBodyBlock>()
            .WithOne()
            .HasForeignKey<ParagraphBodyBlock>(x => new { x.PostId, x.Order })
            .HasConstraintName("FK_bb_paragraph_bb___post_id_order")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
