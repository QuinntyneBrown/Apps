using MarriageEnrichmentJournal.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarriageEnrichmentJournal.Api.Features.Gratitudes;

public record GetGratitudeByIdQuery : IRequest<GratitudeDto?>
{
    public Guid GratitudeId { get; init; }
}

public class GetGratitudeByIdQueryHandler : IRequestHandler<GetGratitudeByIdQuery, GratitudeDto?>
{
    private readonly IMarriageEnrichmentJournalContext _context;
    private readonly ILogger<GetGratitudeByIdQueryHandler> _logger;

    public GetGratitudeByIdQueryHandler(
        IMarriageEnrichmentJournalContext context,
        ILogger<GetGratitudeByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GratitudeDto?> Handle(GetGratitudeByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting gratitude {GratitudeId}", request.GratitudeId);

        var gratitude = await _context.Gratitudes
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.GratitudeId == request.GratitudeId, cancellationToken);

        return gratitude?.ToDto();
    }
}
