using MarriageEnrichmentJournal.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarriageEnrichmentJournal.Api.Features.Reflections;

public record GetReflectionByIdQuery : IRequest<ReflectionDto?>
{
    public Guid ReflectionId { get; init; }
}

public class GetReflectionByIdQueryHandler : IRequestHandler<GetReflectionByIdQuery, ReflectionDto?>
{
    private readonly IMarriageEnrichmentJournalContext _context;
    private readonly ILogger<GetReflectionByIdQueryHandler> _logger;

    public GetReflectionByIdQueryHandler(
        IMarriageEnrichmentJournalContext context,
        ILogger<GetReflectionByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReflectionDto?> Handle(GetReflectionByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reflection {ReflectionId}", request.ReflectionId);

        var reflection = await _context.Reflections
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ReflectionId == request.ReflectionId, cancellationToken);

        return reflection?.ToDto();
    }
}
