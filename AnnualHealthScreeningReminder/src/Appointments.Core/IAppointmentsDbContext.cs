using Microsoft.EntityFrameworkCore;
using Appointments.Core.Models;

namespace Appointments.Core;

public interface IAppointmentsDbContext
{
    DbSet<Appointment> Appointments { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
