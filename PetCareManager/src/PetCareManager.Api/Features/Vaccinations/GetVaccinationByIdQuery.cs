using PetCareManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.Vaccinations;

public record GetVaccinationByIdQuery : IRequest<VaccinationDto?>
{
    public Guid VaccinationId { get; init; }
}

public class GetVaccinationByIdQueryHandler : IRequestHandler<GetVaccinationByIdQuery, VaccinationDto?>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<GetVaccinationByIdQueryHandler> _logger;

    public GetVaccinationByIdQueryHandler(
        IPetCareManagerContext context,
        ILogger<GetVaccinationByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<VaccinationDto?> Handle(GetVaccinationByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vaccination {VaccinationId}", request.VaccinationId);

        var vaccination = await _context.Vaccinations
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.VaccinationId == request.VaccinationId, cancellationToken);

        return vaccination?.ToDto();
    }
}
