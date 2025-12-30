using StressMoodTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StressMoodTracker.Api.Features.Triggers;

public record GetTriggerByIdQuery : IRequest<TriggerDto?>
{
    public Guid TriggerId { get; init; }
}

public class GetTriggerByIdQueryHandler : IRequestHandler<GetTriggerByIdQuery, TriggerDto?>
{
    private readonly IStressMoodTrackerContext _context;
    private readonly ILogger<GetTriggerByIdQueryHandler> _logger;

    public GetTriggerByIdQueryHandler(
        IStressMoodTrackerContext context,
        ILogger<GetTriggerByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TriggerDto?> Handle(GetTriggerByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting trigger {TriggerId}", request.TriggerId);

        var trigger = await _context.Triggers
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TriggerId == request.TriggerId, cancellationToken);

        return trigger?.ToDto();
    }
}
