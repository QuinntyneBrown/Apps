using Distractions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Distractions.Api.Features;

public record UpdateDistractionCommand : IRequest<DistractionDto?>
{
    public Guid DistractionId { get; init; }
    public string? Type { get; init; }
    public string? Description { get; init; }
    public int? DurationSeconds { get; init; }
}

public class UpdateDistractionCommandHandler : IRequestHandler<UpdateDistractionCommand, DistractionDto?>
{
    private readonly IDistractionsDbContext _context;
    private readonly ILogger<UpdateDistractionCommandHandler> _logger;

    public UpdateDistractionCommandHandler(IDistractionsDbContext context, ILogger<UpdateDistractionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DistractionDto?> Handle(UpdateDistractionCommand request, CancellationToken cancellationToken)
    {
        var distraction = await _context.Distractions
            .FirstOrDefaultAsync(d => d.DistractionId == request.DistractionId, cancellationToken);

        if (distraction == null) return null;

        distraction.Update(request.Type, request.Description, request.DurationSeconds);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Distraction updated: {DistractionId}", distraction.DistractionId);

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
