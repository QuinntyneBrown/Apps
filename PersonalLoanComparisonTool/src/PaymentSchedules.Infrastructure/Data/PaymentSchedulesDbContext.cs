using PaymentSchedules.Core;
using PaymentSchedules.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace PaymentSchedules.Infrastructure.Data;

public class PaymentSchedulesDbContext : DbContext, IPaymentSchedulesDbContext
{
    public PaymentSchedulesDbContext(DbContextOptions<PaymentSchedulesDbContext> options) : base(options)
    {
    }

    public DbSet<PaymentSchedule> PaymentSchedules { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PaymentSchedule>(entity =>
        {
            entity.ToTable("PaymentSchedules");
            entity.HasKey(e => e.ScheduleId);
            entity.Property(e => e.PrincipalAmount).HasPrecision(18, 2);
            entity.Property(e => e.InterestAmount).HasPrecision(18, 2);
            entity.Property(e => e.TotalPayment).HasPrecision(18, 2);
            entity.Property(e => e.RemainingBalance).HasPrecision(18, 2);
        });
    }
}
