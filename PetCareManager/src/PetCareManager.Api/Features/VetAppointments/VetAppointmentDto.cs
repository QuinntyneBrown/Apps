using PetCareManager.Core;

namespace PetCareManager.Api.Features.VetAppointments;

public record VetAppointmentDto
{
    public Guid VetAppointmentId { get; init; }
    public Guid PetId { get; init; }
    public DateTime AppointmentDate { get; init; }
    public string? VetName { get; init; }
    public string? Reason { get; init; }
    public string? Notes { get; init; }
    public decimal? Cost { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class VetAppointmentExtensions
{
    public static VetAppointmentDto ToDto(this VetAppointment appointment)
    {
        return new VetAppointmentDto
        {
            VetAppointmentId = appointment.VetAppointmentId,
            PetId = appointment.PetId,
            AppointmentDate = appointment.AppointmentDate,
            VetName = appointment.VetName,
            Reason = appointment.Reason,
            Notes = appointment.Notes,
            Cost = appointment.Cost,
            CreatedAt = appointment.CreatedAt,
        };
    }
}
