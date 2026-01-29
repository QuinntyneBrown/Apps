using Bookings.Core;
using Bookings.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Data;

public class BookingsDbContext : DbContext, IBookingsDbContext
{
    public BookingsDbContext(DbContextOptions<BookingsDbContext> options) : base(options)
    {
    }

    public DbSet<Booking> Bookings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Bookings");
            entity.HasKey(e => e.BookingId);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(100);
            entity.Property(e => e.ConfirmationNumber).HasMaxLength(100);
            entity.Property(e => e.Cost).HasPrecision(18, 2);
        });
    }
}
