using FocusSessions.Core;
using FocusSessions.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FocusSessions.Infrastructure.Data;

public class FocusSessionsDbContext : DbContext, IFocusSessionsDbContext
{
    public FocusSessionsDbContext(DbContextOptions<FocusSessionsDbContext> options) : base(options)
    {
    }

    public DbSet<FocusSession> FocusSessions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FocusSession>(entity =>
        {
            entity.ToTable("FocusSessions");
            entity.HasKey(e => e.SessionId);
            entity.Property(e => e.Status).HasConversion<string>();
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.TenantId);
        });
    }
}
