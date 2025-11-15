using Cms.PostService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Cms.PostService.Infrastructure.Persistence.Configurations;

internal class PostEntityConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("post");
        builder.HasKey(x => x.Id).HasName("PK_post___id");

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator((_, _) => new GuidValueGenerator());

        builder.Property(x => x.BodyPlainText).IsRequired();
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.ListingImageId).IsRequired();
        builder.Property(x => x.TopicId).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Slug).IsRequired();
        builder.Property(x => x.LastModified).IsRequired();

        builder.OwnsMany(x => x.Routes, b => b.ToJson());

        builder.HasIndex(x => x.ListingImageId).HasDatabaseName("IX_post___listing_image_id");
        builder.HasIndex(x => x.TopicId).HasDatabaseName("IX_post___topic_id");

        builder.HasOne(x => x.Topic).WithMany().HasForeignKey(x => x.TopicId).IsRequired();
    }
}
