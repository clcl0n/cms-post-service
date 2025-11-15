using Cms.PostService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Cms.PostService.Infrastructure.Persistence.Configurations;

internal class TopicEntityConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.ToTable("topic");
        builder.HasKey(x => x.Id).HasName("PK_topic___id");

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator((_, _) => new GuidValueGenerator());

        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Slug).IsRequired();

        builder.OwnsMany(x => x.Routes, b => b.ToJson());
    }
}
