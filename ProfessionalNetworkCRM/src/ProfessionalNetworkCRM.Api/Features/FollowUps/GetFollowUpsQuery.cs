using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.FollowUps;

public record GetFollowUpsQuery : IRequest<IEnumerable<FollowUpDto>>
{
    public Guid? UserId { get; init; }
    public Guid? ContactId { get; init; }
    public bool? IsCompleted { get; init; }
    public bool? IsOverdue { get; init; }
}

public class GetFollowUpsQueryHandler : IRequestHandler<GetFollowUpsQuery, IEnumerable<FollowUpDto>>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<GetFollowUpsQueryHandler> _logger;

    public GetFollowUpsQueryHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<GetFollowUpsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<FollowUpDto>> Handle(GetFollowUpsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting follow-ups for user {UserId}, contact {ContactId}", request.UserId, request.ContactId);

        var query = _context.FollowUps.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(f => f.UserId == request.UserId.Value);
        }

        if (request.ContactId.HasValue)
        {
            query = query.Where(f => f.ContactId == request.ContactId.Value);
        }

        if (request.IsCompleted.HasValue)
        {
            query = query.Where(f => f.IsCompleted == request.IsCompleted.Value);
        }

        var followUps = await query
            .OrderBy(f => f.DueDate)
            .ToListAsync(cancellationToken);

        // Filter by IsOverdue if requested (done in memory since IsOverdue is computed)
        if (request.IsOverdue.HasValue)
        {
            followUps = followUps.Where(f => f.IsOverdue() == request.IsOverdue.Value).ToList();
        }

        return followUps.Select(f => f.ToDto());
    }
}
