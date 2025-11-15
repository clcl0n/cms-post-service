using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cms.PostService.Infrastructure.Persistence;

internal sealed class PostServiceDbContext(
    DbContextOptions options,
    ILogger<PostServiceDbContext> logger
) : DbContext(options)
{
    public async Task ApplyMigrations(CancellationToken cancellationToken)
    {
        var pendingMigrations = await Database.GetPendingMigrationsAsync(cancellationToken);
        var pendingList = pendingMigrations.ToList();

        if (pendingList.Count == 0)
        {
            logger.LogInformation("No pending migrations to apply");
            return;
        }

        logger.LogInformation("Found {Count} pending migrations", pendingList.Count);

        try
        {
            // Log all pending migrations before applying
            foreach (var migration in pendingList)
            {
                logger.LogInformation("Preparing to apply migration: {Migration}", migration);
            }

            // Apply all migrations in one go
            logger.LogInformation("Applying all pending migrations...");
            await Database.MigrateAsync(cancellationToken);

            logger.LogInformation("Successfully applied {Count} migrations", pendingList.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to apply migrations");

            throw;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("cms-post-service");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostServiceDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
    }
}
