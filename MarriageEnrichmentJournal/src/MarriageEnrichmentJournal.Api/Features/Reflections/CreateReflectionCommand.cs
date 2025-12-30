using MarriageEnrichmentJournal.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarriageEnrichmentJournal.Api.Features.Reflections;

public record CreateReflectionCommand : IRequest<ReflectionDto>
{
    public Guid? JournalEntryId { get; init; }
    public Guid UserId { get; init; }
    public string Text { get; init; } = string.Empty;
    public string? Topic { get; init; }
    public DateTime ReflectionDate { get; init; }
}

public class CreateReflectionCommandHandler : IRequestHandler<CreateReflectionCommand, ReflectionDto>
{
    private readonly IMarriageEnrichmentJournalContext _context;
    private readonly ILogger<CreateReflectionCommandHandler> _logger;

    public CreateReflectionCommandHandler(
        IMarriageEnrichmentJournalContext context,
        ILogger<CreateReflectionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReflectionDto> Handle(CreateReflectionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating reflection for user {UserId}",
            request.UserId);

        var reflection = new Reflection
        {
            ReflectionId = Guid.NewGuid(),
            JournalEntryId = request.JournalEntryId,
            UserId = request.UserId,
            Text = request.Text,
            Topic = request.Topic,
            ReflectionDate = request.ReflectionDate,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Reflections.Add(reflection);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created reflection {ReflectionId} for user {UserId}",
            reflection.ReflectionId,
            request.UserId);

        return reflection.ToDto();
    }
}
