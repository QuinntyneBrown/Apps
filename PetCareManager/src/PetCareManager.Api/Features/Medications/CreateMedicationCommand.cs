using PetCareManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.Medications;

public record CreateMedicationCommand : IRequest<MedicationDto>
{
    public Guid PetId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Dosage { get; init; }
    public string? Frequency { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class CreateMedicationCommandHandler : IRequestHandler<CreateMedicationCommand, MedicationDto>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<CreateMedicationCommandHandler> _logger;

    public CreateMedicationCommandHandler(
        IPetCareManagerContext context,
        ILogger<CreateMedicationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MedicationDto> Handle(CreateMedicationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating medication for pet {PetId}, name: {Name}",
            request.PetId,
            request.Name);

        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            PetId = request.PetId,
            Name = request.Name,
            Dosage = request.Dosage,
            Frequency = request.Frequency,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Medications.Add(medication);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created medication {MedicationId} for pet {PetId}",
            medication.MedicationId,
            request.PetId);

        return medication.ToDto();
    }
}
