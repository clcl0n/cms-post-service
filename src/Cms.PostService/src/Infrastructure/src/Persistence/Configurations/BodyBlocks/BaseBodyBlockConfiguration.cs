using Cms.PostService.Domain.Entities.BodyBlocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.PostService.Infrastructure.Persistence.Configurations.BodyBlocks;

internal class BaseBodyBlockConfiguration : IEntityTypeConfiguration<BaseBodyBlock>
{
    public void Configure(EntityTypeBuilder<BaseBodyBlock> builder)
    {
        builder.UseTptMappingStrategy();

        builder.ToTable("body_block");
        builder.HasKey(x => new { x.PostId, x.Order });

        builder.HasIndex(x => x.PostId).HasDatabaseName("IX_bb___post_id");

        builder
            .HasOne(x => x.Post)
            .WithMany(x => x.BodyBlocks)
            .HasForeignKey(x => x.PostId)
            .HasConstraintName("FK_bb_post___post_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
