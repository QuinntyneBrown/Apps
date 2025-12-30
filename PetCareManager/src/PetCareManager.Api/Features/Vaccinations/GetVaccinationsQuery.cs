using PetCareManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.Vaccinations;

public record GetVaccinationsQuery : IRequest<IEnumerable<VaccinationDto>>
{
    public Guid? PetId { get; init; }
}

public class GetVaccinationsQueryHandler : IRequestHandler<GetVaccinationsQuery, IEnumerable<VaccinationDto>>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<GetVaccinationsQueryHandler> _logger;

    public GetVaccinationsQueryHandler(
        IPetCareManagerContext context,
        ILogger<GetVaccinationsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<VaccinationDto>> Handle(GetVaccinationsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vaccinations for pet {PetId}", request.PetId);

        var query = _context.Vaccinations.AsNoTracking();

        if (request.PetId.HasValue)
        {
            query = query.Where(v => v.PetId == request.PetId.Value);
        }

        var vaccinations = await query
            .OrderByDescending(v => v.DateAdministered)
            .ToListAsync(cancellationToken);

        return vaccinations.Select(v => v.ToDto());
    }
}
