using Appointments.Core;
using Appointments.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Infrastructure.Data;

public class AppointmentsDbContext : DbContext, IAppointmentsDbContext
{
    public AppointmentsDbContext(DbContextOptions<AppointmentsDbContext> options) : base(options)
    {
    }

    public DbSet<Appointment> Appointments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.ToTable("Appointments");
            entity.HasKey(e => e.AppointmentId);
            entity.Property(e => e.ProviderName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.AppointmentType).IsRequired().HasMaxLength(100);
        });
    }
}
