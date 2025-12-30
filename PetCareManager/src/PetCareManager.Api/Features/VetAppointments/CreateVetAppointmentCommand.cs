using PetCareManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.VetAppointments;

public record CreateVetAppointmentCommand : IRequest<VetAppointmentDto>
{
    public Guid PetId { get; init; }
    public DateTime AppointmentDate { get; init; }
    public string? VetName { get; init; }
    public string? Reason { get; init; }
    public string? Notes { get; init; }
    public decimal? Cost { get; init; }
}

public class CreateVetAppointmentCommandHandler : IRequestHandler<CreateVetAppointmentCommand, VetAppointmentDto>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<CreateVetAppointmentCommandHandler> _logger;

    public CreateVetAppointmentCommandHandler(
        IPetCareManagerContext context,
        ILogger<CreateVetAppointmentCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<VetAppointmentDto> Handle(CreateVetAppointmentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating vet appointment for pet {PetId} on {AppointmentDate}",
            request.PetId,
            request.AppointmentDate);

        var appointment = new VetAppointment
        {
            VetAppointmentId = Guid.NewGuid(),
            PetId = request.PetId,
            AppointmentDate = request.AppointmentDate,
            VetName = request.VetName,
            Reason = request.Reason,
            Notes = request.Notes,
            Cost = request.Cost,
            CreatedAt = DateTime.UtcNow,
        };

        _context.VetAppointments.Add(appointment);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created vet appointment {VetAppointmentId} for pet {PetId}",
            appointment.VetAppointmentId,
            request.PetId);

        return appointment.ToDto();
    }
}
