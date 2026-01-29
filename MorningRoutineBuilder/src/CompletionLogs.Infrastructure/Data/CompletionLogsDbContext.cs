using CompletionLogs.Core;
using CompletionLogs.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CompletionLogs.Infrastructure.Data;

public class CompletionLogsDbContext : DbContext, ICompletionLogsDbContext
{
    public CompletionLogsDbContext(DbContextOptions<CompletionLogsDbContext> options) : base(options) { }
    public DbSet<CompletionLog> CompletionLogs { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompletionLog>(e => {
            e.ToTable("CompletionLogs");
            e.HasKey(x => x.CompletionLogId);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasIndex(x => new { x.TenantId, x.UserId });
        });
    }
}
