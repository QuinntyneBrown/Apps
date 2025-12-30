using MarriageEnrichmentJournal.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarriageEnrichmentJournal.Api.Features.Gratitudes;

public record CreateGratitudeCommand : IRequest<GratitudeDto>
{
    public Guid? JournalEntryId { get; init; }
    public Guid UserId { get; init; }
    public string Text { get; init; } = string.Empty;
    public DateTime GratitudeDate { get; init; }
}

public class CreateGratitudeCommandHandler : IRequestHandler<CreateGratitudeCommand, GratitudeDto>
{
    private readonly IMarriageEnrichmentJournalContext _context;
    private readonly ILogger<CreateGratitudeCommandHandler> _logger;

    public CreateGratitudeCommandHandler(
        IMarriageEnrichmentJournalContext context,
        ILogger<CreateGratitudeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GratitudeDto> Handle(CreateGratitudeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating gratitude for user {UserId}",
            request.UserId);

        var gratitude = new Gratitude
        {
            GratitudeId = Guid.NewGuid(),
            JournalEntryId = request.JournalEntryId,
            UserId = request.UserId,
            Text = request.Text,
            GratitudeDate = request.GratitudeDate,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Gratitudes.Add(gratitude);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created gratitude {GratitudeId} for user {UserId}",
            gratitude.GratitudeId,
            request.UserId);

        return gratitude.ToDto();
    }
}
