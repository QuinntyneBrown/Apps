using MarriageEnrichmentJournal.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarriageEnrichmentJournal.Api.Features.Reflections;

public record GetReflectionsQuery : IRequest<IEnumerable<ReflectionDto>>
{
    public Guid? UserId { get; init; }
    public Guid? JournalEntryId { get; init; }
    public string? Topic { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class GetReflectionsQueryHandler : IRequestHandler<GetReflectionsQuery, IEnumerable<ReflectionDto>>
{
    private readonly IMarriageEnrichmentJournalContext _context;
    private readonly ILogger<GetReflectionsQueryHandler> _logger;

    public GetReflectionsQueryHandler(
        IMarriageEnrichmentJournalContext context,
        ILogger<GetReflectionsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ReflectionDto>> Handle(GetReflectionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reflections for user {UserId}", request.UserId);

        var query = _context.Reflections.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == request.UserId.Value);
        }

        if (request.JournalEntryId.HasValue)
        {
            query = query.Where(r => r.JournalEntryId == request.JournalEntryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Topic))
        {
            query = query.Where(r => r.Topic == request.Topic);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(r => r.ReflectionDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(r => r.ReflectionDate <= request.EndDate.Value);
        }

        var reflections = await query
            .OrderByDescending(r => r.ReflectionDate)
            .ToListAsync(cancellationToken);

        return reflections.Select(r => r.ToDto());
    }
}
