using PlaySessions.Core;
using PlaySessions.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace PlaySessions.Infrastructure.Data;

public class PlaySessionsDbContext : DbContext, IPlaySessionsDbContext
{
    public PlaySessionsDbContext(DbContextOptions<PlaySessionsDbContext> options) : base(options)
    {
    }

    public DbSet<PlaySession> PlaySessions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PlaySession>(entity =>
        {
            entity.ToTable("PlaySessions");
            entity.HasKey(e => e.SessionId);
        });
    }
}
