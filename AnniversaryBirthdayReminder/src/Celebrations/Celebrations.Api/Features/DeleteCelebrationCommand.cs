using Celebrations.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Celebrations.Api.Features;

public record DeleteCelebrationCommand(Guid CelebrationId) : IRequest<bool>;

public class DeleteCelebrationCommandHandler : IRequestHandler<DeleteCelebrationCommand, bool>
{
    private readonly ICelebrationsDbContext _context;
    private readonly ILogger<DeleteCelebrationCommandHandler> _logger;

    public DeleteCelebrationCommandHandler(ICelebrationsDbContext context, ILogger<DeleteCelebrationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCelebrationCommand request, CancellationToken cancellationToken)
    {
        var celebration = await _context.Celebrations
            .FirstOrDefaultAsync(c => c.CelebrationId == request.CelebrationId, cancellationToken);

        if (celebration == null) return false;

        _context.Celebrations.Remove(celebration);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Celebration deleted: {CelebrationId}", request.CelebrationId);
        return true;
    }
}
