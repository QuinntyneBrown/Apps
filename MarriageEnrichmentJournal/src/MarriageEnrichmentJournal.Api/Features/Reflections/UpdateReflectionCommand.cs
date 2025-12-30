using MarriageEnrichmentJournal.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarriageEnrichmentJournal.Api.Features.Reflections;

public record UpdateReflectionCommand : IRequest<ReflectionDto?>
{
    public Guid ReflectionId { get; init; }
    public string Text { get; init; } = string.Empty;
    public string? Topic { get; init; }
    public DateTime ReflectionDate { get; init; }
}

public class UpdateReflectionCommandHandler : IRequestHandler<UpdateReflectionCommand, ReflectionDto?>
{
    private readonly IMarriageEnrichmentJournalContext _context;
    private readonly ILogger<UpdateReflectionCommandHandler> _logger;

    public UpdateReflectionCommandHandler(
        IMarriageEnrichmentJournalContext context,
        ILogger<UpdateReflectionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReflectionDto?> Handle(UpdateReflectionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating reflection {ReflectionId}", request.ReflectionId);

        var reflection = await _context.Reflections
            .FirstOrDefaultAsync(r => r.ReflectionId == request.ReflectionId, cancellationToken);

        if (reflection == null)
        {
            _logger.LogWarning("Reflection {ReflectionId} not found", request.ReflectionId);
            return null;
        }

        reflection.Text = request.Text;
        reflection.Topic = request.Topic;
        reflection.ReflectionDate = request.ReflectionDate;
        reflection.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated reflection {ReflectionId}", request.ReflectionId);

        return reflection.ToDto();
    }
}
