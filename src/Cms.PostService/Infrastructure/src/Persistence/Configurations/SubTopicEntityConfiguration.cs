using Cms.PostService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Cms.PostService.Infrastructure.Persistence.Configurations;

internal class SubTopicEntityConfiguration : IEntityTypeConfiguration<SubTopic>
{
    public void Configure(EntityTypeBuilder<SubTopic> builder)
    {
        builder.ToTable("subtopic");
        builder.HasKey(x => x.Id).HasName("PK_subtopic___id");

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator((_, _) => new GuidValueGenerator());

        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Slug).IsRequired();

        builder.OwnsMany(x => x.Routes, b => b.ToJson());

        builder
            .HasOne(x => x.ParentTopic)
            .WithMany(x => x.SubTopics)
            .HasForeignKey(x => x.ParentTopicId)
            .IsRequired();
    }
}
