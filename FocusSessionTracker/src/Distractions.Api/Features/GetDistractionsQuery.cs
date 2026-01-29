using Distractions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Distractions.Api.Features;

public record GetDistractionsQuery : IRequest<IEnumerable<DistractionDto>>
{
    public Guid? SessionId { get; init; }
}

public class GetDistractionsQueryHandler : IRequestHandler<GetDistractionsQuery, IEnumerable<DistractionDto>>
{
    private readonly IDistractionsDbContext _context;

    public GetDistractionsQueryHandler(IDistractionsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DistractionDto>> Handle(GetDistractionsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Distractions.AsNoTracking();

        if (request.SessionId.HasValue)
        {
            query = query.Where(d => d.SessionId == request.SessionId.Value);
        }

        return await query
            .OrderByDescending(d => d.OccurredAt)
            .Select(d => new DistractionDto
            {
                DistractionId = d.DistractionId,
                SessionId = d.SessionId,
                Type = d.Type,
                Description = d.Description,
                OccurredAt = d.OccurredAt,
                DurationSeconds = d.DurationSeconds
            })
            .ToListAsync(cancellationToken);
    }
}
