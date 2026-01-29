using Distractions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Distractions.Api.Features;

public record GetDistractionByIdQuery : IRequest<DistractionDto?>
{
    public Guid DistractionId { get; init; }
}

public class GetDistractionByIdQueryHandler : IRequestHandler<GetDistractionByIdQuery, DistractionDto?>
{
    private readonly IDistractionsDbContext _context;

    public GetDistractionByIdQueryHandler(IDistractionsDbContext context)
    {
        _context = context;
    }

    public async Task<DistractionDto?> Handle(GetDistractionByIdQuery request, CancellationToken cancellationToken)
    {
        var distraction = await _context.Distractions
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DistractionId == request.DistractionId, cancellationToken);

        if (distraction == null) return null;

        return new DistractionDto
        {
            DistractionId = distraction.DistractionId,
            SessionId = distraction.SessionId,
            Type = distraction.Type,
            Description = distraction.Description,
            OccurredAt = distraction.OccurredAt,
            DurationSeconds = distraction.DurationSeconds
        };
    }
}
