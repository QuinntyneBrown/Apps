using BucketListManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BucketListManager.Api.Features.Milestones;

public record GetMilestoneByIdQuery : IRequest<MilestoneDto?>
{
    public Guid MilestoneId { get; init; }
}

public class GetMilestoneByIdQueryHandler : IRequestHandler<GetMilestoneByIdQuery, MilestoneDto?>
{
    private readonly IBucketListManagerContext _context;
    private readonly ILogger<GetMilestoneByIdQueryHandler> _logger;

    public GetMilestoneByIdQueryHandler(
        IBucketListManagerContext context,
        ILogger<GetMilestoneByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MilestoneDto?> Handle(GetMilestoneByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting milestone {MilestoneId}", request.MilestoneId);

        var milestone = await _context.Milestones
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.MilestoneId == request.MilestoneId, cancellationToken);

        return milestone?.ToDto();
    }
}
