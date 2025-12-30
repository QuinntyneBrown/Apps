using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Gears;

public record GetGearByIdQuery : IRequest<GearDto?>
{
    public Guid GearId { get; init; }
}

public class GetGearByIdQueryHandler : IRequestHandler<GetGearByIdQuery, GearDto?>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<GetGearByIdQueryHandler> _logger;

    public GetGearByIdQueryHandler(
        IPhotographySessionLoggerContext context,
        ILogger<GetGearByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GearDto?> Handle(GetGearByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting gear {GearId}", request.GearId);

        var gear = await _context.Gears
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.GearId == request.GearId, cancellationToken);

        return gear?.ToDto();
    }
}
