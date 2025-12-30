using MarriageEnrichmentJournal.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarriageEnrichmentJournal.Api.Features.Reflections;

public record DeleteReflectionCommand : IRequest<bool>
{
    public Guid ReflectionId { get; init; }
}

public class DeleteReflectionCommandHandler : IRequestHandler<DeleteReflectionCommand, bool>
{
    private readonly IMarriageEnrichmentJournalContext _context;
    private readonly ILogger<DeleteReflectionCommandHandler> _logger;

    public DeleteReflectionCommandHandler(
        IMarriageEnrichmentJournalContext context,
        ILogger<DeleteReflectionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteReflectionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting reflection {ReflectionId}", request.ReflectionId);

        var reflection = await _context.Reflections
            .FirstOrDefaultAsync(r => r.ReflectionId == request.ReflectionId, cancellationToken);

        if (reflection == null)
        {
            _logger.LogWarning("Reflection {ReflectionId} not found", request.ReflectionId);
            return false;
        }

        _context.Reflections.Remove(reflection);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted reflection {ReflectionId}", request.ReflectionId);

        return true;
    }
}
