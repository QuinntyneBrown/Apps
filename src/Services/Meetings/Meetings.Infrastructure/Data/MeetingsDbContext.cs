using Meetings.Core;
using Meetings.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Infrastructure.Data;

public class MeetingsDbContext : DbContext, IMeetingsDbContext
{
    public MeetingsDbContext(DbContextOptions<MeetingsDbContext> options) : base(options) { }
    public DbSet<Meeting> Meetings { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meeting>(e => {
            e.ToTable("Meetings");
            e.HasKey(x => x.MeetingId);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasIndex(x => new { x.TenantId, x.UserId });
        });
    }
}
