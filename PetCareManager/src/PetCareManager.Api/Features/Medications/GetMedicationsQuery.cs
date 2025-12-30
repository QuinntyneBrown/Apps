using PetCareManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.Medications;

public record GetMedicationsQuery : IRequest<IEnumerable<MedicationDto>>
{
    public Guid? PetId { get; init; }
}

public class GetMedicationsQueryHandler : IRequestHandler<GetMedicationsQuery, IEnumerable<MedicationDto>>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<GetMedicationsQueryHandler> _logger;

    public GetMedicationsQueryHandler(
        IPetCareManagerContext context,
        ILogger<GetMedicationsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MedicationDto>> Handle(GetMedicationsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting medications for pet {PetId}", request.PetId);

        var query = _context.Medications.AsNoTracking();

        if (request.PetId.HasValue)
        {
            query = query.Where(m => m.PetId == request.PetId.Value);
        }

        var medications = await query
            .OrderByDescending(m => m.StartDate)
            .ToListAsync(cancellationToken);

        return medications.Select(m => m.ToDto());
    }
}
