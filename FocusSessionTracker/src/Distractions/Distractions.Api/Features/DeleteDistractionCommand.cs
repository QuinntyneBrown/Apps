using Distractions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Distractions.Api.Features;

public record DeleteDistractionCommand : IRequest<bool>
{
    public Guid DistractionId { get; init; }
}

public class DeleteDistractionCommandHandler : IRequestHandler<DeleteDistractionCommand, bool>
{
    private readonly IDistractionsDbContext _context;
    private readonly ILogger<DeleteDistractionCommandHandler> _logger;

    public DeleteDistractionCommandHandler(IDistractionsDbContext context, ILogger<DeleteDistractionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteDistractionCommand request, CancellationToken cancellationToken)
    {
        var distraction = await _context.Distractions
            .FirstOrDefaultAsync(d => d.DistractionId == request.DistractionId, cancellationToken);

        if (distraction == null) return false;

        _context.Distractions.Remove(distraction);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Distraction deleted: {DistractionId}", request.DistractionId);

        return true;
    }
}
