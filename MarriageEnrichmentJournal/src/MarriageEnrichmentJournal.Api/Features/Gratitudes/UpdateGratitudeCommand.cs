using MarriageEnrichmentJournal.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarriageEnrichmentJournal.Api.Features.Gratitudes;

public record UpdateGratitudeCommand : IRequest<GratitudeDto?>
{
    public Guid GratitudeId { get; init; }
    public string Text { get; init; } = string.Empty;
    public DateTime GratitudeDate { get; init; }
}

public class UpdateGratitudeCommandHandler : IRequestHandler<UpdateGratitudeCommand, GratitudeDto?>
{
    private readonly IMarriageEnrichmentJournalContext _context;
    private readonly ILogger<UpdateGratitudeCommandHandler> _logger;

    public UpdateGratitudeCommandHandler(
        IMarriageEnrichmentJournalContext context,
        ILogger<UpdateGratitudeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GratitudeDto?> Handle(UpdateGratitudeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating gratitude {GratitudeId}", request.GratitudeId);

        var gratitude = await _context.Gratitudes
            .FirstOrDefaultAsync(g => g.GratitudeId == request.GratitudeId, cancellationToken);

        if (gratitude == null)
        {
            _logger.LogWarning("Gratitude {GratitudeId} not found", request.GratitudeId);
            return null;
        }

        gratitude.Text = request.Text;
        gratitude.GratitudeDate = request.GratitudeDate;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated gratitude {GratitudeId}", request.GratitudeId);

        return gratitude.ToDto();
    }
}
