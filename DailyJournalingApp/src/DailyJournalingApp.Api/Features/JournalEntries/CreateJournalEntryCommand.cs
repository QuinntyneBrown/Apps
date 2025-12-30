using DailyJournalingApp.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DailyJournalingApp.Api.Features.JournalEntries;

public record CreateJournalEntryCommand : IRequest<JournalEntryDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime EntryDate { get; init; }
    public Mood Mood { get; init; }
    public Guid? PromptId { get; init; }
    public string? Tags { get; init; }
}

public class CreateJournalEntryCommandHandler : IRequestHandler<CreateJournalEntryCommand, JournalEntryDto>
{
    private readonly IDailyJournalingAppContext _context;
    private readonly ILogger<CreateJournalEntryCommandHandler> _logger;

    public CreateJournalEntryCommandHandler(
        IDailyJournalingAppContext context,
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
            EntryDate = request.EntryDate,
            Mood = request.Mood,
            PromptId = request.PromptId,
            Tags = request.Tags,
            IsFavorite = false,
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
