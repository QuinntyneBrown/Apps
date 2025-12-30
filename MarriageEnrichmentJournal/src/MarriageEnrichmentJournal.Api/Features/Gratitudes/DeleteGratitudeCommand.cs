using MarriageEnrichmentJournal.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarriageEnrichmentJournal.Api.Features.Gratitudes;

public record DeleteGratitudeCommand : IRequest<bool>
{
    public Guid GratitudeId { get; init; }
}

public class DeleteGratitudeCommandHandler : IRequestHandler<DeleteGratitudeCommand, bool>
{
    private readonly IMarriageEnrichmentJournalContext _context;
    private readonly ILogger<DeleteGratitudeCommandHandler> _logger;

    public DeleteGratitudeCommandHandler(
        IMarriageEnrichmentJournalContext context,
        ILogger<DeleteGratitudeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteGratitudeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting gratitude {GratitudeId}", request.GratitudeId);

        var gratitude = await _context.Gratitudes
            .FirstOrDefaultAsync(g => g.GratitudeId == request.GratitudeId, cancellationToken);

        if (gratitude == null)
        {
            _logger.LogWarning("Gratitude {GratitudeId} not found", request.GratitudeId);
            return false;
        }

        _context.Gratitudes.Remove(gratitude);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted gratitude {GratitudeId}", request.GratitudeId);

        return true;
    }
}
