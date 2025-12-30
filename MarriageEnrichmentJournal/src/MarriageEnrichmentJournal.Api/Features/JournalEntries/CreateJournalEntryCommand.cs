using MarriageEnrichmentJournal.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarriageEnrichmentJournal.Api.Features.JournalEntries;

public record CreateJournalEntryCommand : IRequest<JournalEntryDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public EntryType EntryType { get; init; }
    public DateTime EntryDate { get; init; }
    public bool IsSharedWithPartner { get; init; }
    public bool IsPrivate { get; init; }
    public string? Tags { get; init; }
}

public class CreateJournalEntryCommandHandler : IRequestHandler<CreateJournalEntryCommand, JournalEntryDto>
{
    private readonly IMarriageEnrichmentJournalContext _context;
    private readonly ILogger<CreateJournalEntryCommandHandler> _logger;

    public CreateJournalEntryCommandHandler(
        IMarriageEnrichmentJournalContext context,
        ILogger<CreateJournalEntryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<JournalEntryDto> Handle(CreateJournalEntryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating journal entry for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var entry = new JournalEntry
        {
            JournalEntryId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Content = request.Content,
            EntryType = request.EntryType,
            EntryDate = request.EntryDate,
            IsSharedWithPartner = request.IsSharedWithPartner,
            IsPrivate = request.IsPrivate,
            Tags = request.Tags,
            CreatedAt = DateTime.UtcNow,
        };

        _context.JournalEntries.Add(entry);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created journal entry {JournalEntryId} for user {UserId}",
            entry.JournalEntryId,
            request.UserId);

        return entry.ToDto();
    }
}
