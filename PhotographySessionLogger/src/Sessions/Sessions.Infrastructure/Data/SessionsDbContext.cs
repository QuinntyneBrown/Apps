using Sessions.Core;
using Sessions.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Sessions.Infrastructure.Data;

public class SessionsDbContext : DbContext, ISessionsDbContext
{
    public SessionsDbContext(DbContextOptions<SessionsDbContext> options) : base(options)
    {
    }

    public DbSet<Session> Sessions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("Sessions");
            entity.HasKey(e => e.SessionId);
            entity.Property(e => e.Location).HasMaxLength(500).IsRequired();
            entity.Property(e => e.SessionType).HasMaxLength(100).IsRequired();
            entity.Property(e => e.WeatherConditions).HasMaxLength(200);
        });
    }
}
