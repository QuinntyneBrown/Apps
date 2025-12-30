using StressMoodTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace StressMoodTracker.Api.Features.Journals;

public record CreateJournalCommand : IRequest<JournalDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime EntryDate { get; init; }
    public string? Tags { get; init; }
}

public class CreateJournalCommandHandler : IRequestHandler<CreateJournalCommand, JournalDto>
{
    private readonly IStressMoodTrackerContext _context;
    private readonly ILogger<CreateJournalCommandHandler> _logger;

    public CreateJournalCommandHandler(
        IStressMoodTrackerContext context,
        ILogger<CreateJournalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<JournalDto> Handle(CreateJournalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating journal entry for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var journal = new Journal
        {
            JournalId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Content = request.Content,
            EntryDate = request.EntryDate,
            Tags = request.Tags,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Journals.Add(journal);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created journal entry {JournalId} for user {UserId}",
            journal.JournalId,
            request.UserId);

        return journal.ToDto();
    }
}
