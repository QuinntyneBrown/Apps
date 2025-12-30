using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Gears;

public record DeleteGearCommand : IRequest<bool>
{
    public Guid GearId { get; init; }
}

public class DeleteGearCommandHandler : IRequestHandler<DeleteGearCommand, bool>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<DeleteGearCommandHandler> _logger;

    public DeleteGearCommandHandler(
        IPhotographySessionLoggerContext context,
        ILogger<DeleteGearCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteGearCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting gear {GearId}", request.GearId);

        var gear = await _context.Gears
            .FirstOrDefaultAsync(g => g.GearId == request.GearId, cancellationToken);

        if (gear == null)
        {
            _logger.LogWarning("Gear {GearId} not found", request.GearId);
            return false;
        }

        _context.Gears.Remove(gear);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted gear {GearId}", request.GearId);

        return true;
    }
}
