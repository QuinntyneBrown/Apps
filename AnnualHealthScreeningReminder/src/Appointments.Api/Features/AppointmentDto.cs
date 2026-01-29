using Appointments.Core.Models;

namespace Appointments.Api.Features;

public record AppointmentDto(
    Guid AppointmentId,
    Guid UserId,
    string ProviderName,
    string AppointmentType,
    DateTime AppointmentDate,
    string? Location,
    string? Notes,
    bool IsCompleted,
    DateTime CreatedAt);

public static class AppointmentExtensions
{
    public static AppointmentDto ToDto(this Appointment appointment)
    {
        return new AppointmentDto(
            appointment.AppointmentId,
            appointment.UserId,
            appointment.ProviderName,
            appointment.AppointmentType,
            appointment.AppointmentDate,
            appointment.Location,
            appointment.Notes,
            appointment.IsCompleted,
            appointment.CreatedAt);
    }
}
