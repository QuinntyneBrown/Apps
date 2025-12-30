using PetCareManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.Medications;

public record GetMedicationByIdQuery : IRequest<MedicationDto?>
{
    public Guid MedicationId { get; init; }
}

public class GetMedicationByIdQueryHandler : IRequestHandler<GetMedicationByIdQuery, MedicationDto?>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<GetMedicationByIdQueryHandler> _logger;

    public GetMedicationByIdQueryHandler(
        IPetCareManagerContext context,
        ILogger<GetMedicationByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MedicationDto?> Handle(GetMedicationByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting medication {MedicationId}", request.MedicationId);

        var medication = await _context.Medications
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MedicationId == request.MedicationId, cancellationToken);

        return medication?.ToDto();
    }
}
