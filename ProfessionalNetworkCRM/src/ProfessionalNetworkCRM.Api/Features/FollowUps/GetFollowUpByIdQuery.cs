using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.FollowUps;

public record GetFollowUpByIdQuery : IRequest<FollowUpDto?>
{
    public Guid FollowUpId { get; init; }
}

public class GetFollowUpByIdQueryHandler : IRequestHandler<GetFollowUpByIdQuery, FollowUpDto?>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<GetFollowUpByIdQueryHandler> _logger;

    public GetFollowUpByIdQueryHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<GetFollowUpByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<FollowUpDto?> Handle(GetFollowUpByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting follow-up {FollowUpId}", request.FollowUpId);

        var followUp = await _context.FollowUps
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.FollowUpId == request.FollowUpId, cancellationToken);

        if (followUp == null)
        {
            _logger.LogWarning("Follow-up {FollowUpId} not found", request.FollowUpId);
            return null;
        }

        return followUp.ToDto();
    }
}
