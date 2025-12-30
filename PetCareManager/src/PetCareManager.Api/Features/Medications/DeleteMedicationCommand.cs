using PetCareManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.Medications;

public record DeleteMedicationCommand : IRequest<bool>
{
    public Guid MedicationId { get; init; }
}

public class DeleteMedicationCommandHandler : IRequestHandler<DeleteMedicationCommand, bool>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<DeleteMedicationCommandHandler> _logger;

    public DeleteMedicationCommandHandler(
        IPetCareManagerContext context,
        ILogger<DeleteMedicationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMedicationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting medication {MedicationId}", request.MedicationId);

        var medication = await _context.Medications
            .FirstOrDefaultAsync(m => m.MedicationId == request.MedicationId, cancellationToken);

        if (medication == null)
        {
            _logger.LogWarning("Medication {MedicationId} not found", request.MedicationId);
            return false;
        }

        _context.Medications.Remove(medication);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted medication {MedicationId}", request.MedicationId);

        return true;
    }
}
