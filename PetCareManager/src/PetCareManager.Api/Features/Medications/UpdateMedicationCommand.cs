using PetCareManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.Medications;

public record UpdateMedicationCommand : IRequest<MedicationDto?>
{
    public Guid MedicationId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Dosage { get; init; }
    public string? Frequency { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class UpdateMedicationCommandHandler : IRequestHandler<UpdateMedicationCommand, MedicationDto?>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<UpdateMedicationCommandHandler> _logger;

    public UpdateMedicationCommandHandler(
        IPetCareManagerContext context,
        ILogger<UpdateMedicationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MedicationDto?> Handle(UpdateMedicationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating medication {MedicationId}", request.MedicationId);

        var medication = await _context.Medications
            .FirstOrDefaultAsync(m => m.MedicationId == request.MedicationId, cancellationToken);

        if (medication == null)
        {
            _logger.LogWarning("Medication {MedicationId} not found", request.MedicationId);
            return null;
        }

        medication.Name = request.Name;
        medication.Dosage = request.Dosage;
        medication.Frequency = request.Frequency;
        medication.StartDate = request.StartDate;
        medication.EndDate = request.EndDate;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated medication {MedicationId}", request.MedicationId);

        return medication.ToDto();
    }
}
