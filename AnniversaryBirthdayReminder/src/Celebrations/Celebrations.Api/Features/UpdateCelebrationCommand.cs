using Celebrations.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Celebrations.Api.Features;

public record UpdateCelebrationCommand(
    Guid CelebrationId,
    string Name,
    string CelebrationType,
    DateTime Date,
    string? Notes,
    bool IsRecurring) : IRequest<CelebrationDto?>;

public class UpdateCelebrationCommandHandler : IRequestHandler<UpdateCelebrationCommand, CelebrationDto?>
{
    private readonly ICelebrationsDbContext _context;
    private readonly ILogger<UpdateCelebrationCommandHandler> _logger;

    public UpdateCelebrationCommandHandler(ICelebrationsDbContext context, ILogger<UpdateCelebrationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CelebrationDto?> Handle(UpdateCelebrationCommand request, CancellationToken cancellationToken)
    {
        var celebration = await _context.Celebrations
            .FirstOrDefaultAsync(c => c.CelebrationId == request.CelebrationId, cancellationToken);

        if (celebration == null) return null;

        celebration.Name = request.Name;
        celebration.CelebrationType = request.CelebrationType;
        celebration.Date = request.Date;
        celebration.Notes = request.Notes;
        celebration.IsRecurring = request.IsRecurring;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Celebration updated: {CelebrationId}", celebration.CelebrationId);

        return celebration.ToDto();
    }
}
