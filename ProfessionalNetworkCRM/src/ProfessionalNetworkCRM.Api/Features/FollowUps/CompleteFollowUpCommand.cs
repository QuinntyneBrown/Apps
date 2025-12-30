using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.FollowUps;

public record CompleteFollowUpCommand : IRequest<FollowUpDto?>
{
    public Guid FollowUpId { get; init; }
}

public class CompleteFollowUpCommandHandler : IRequestHandler<CompleteFollowUpCommand, FollowUpDto?>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<CompleteFollowUpCommandHandler> _logger;

    public CompleteFollowUpCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<CompleteFollowUpCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<FollowUpDto?> Handle(CompleteFollowUpCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Completing follow-up {FollowUpId}", request.FollowUpId);

        var followUp = await _context.FollowUps
            .FirstOrDefaultAsync(f => f.FollowUpId == request.FollowUpId, cancellationToken);

        if (followUp == null)
        {
            _logger.LogWarning("Follow-up {FollowUpId} not found", request.FollowUpId);
            return null;
        }

        followUp.Complete();

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Completed follow-up {FollowUpId}", request.FollowUpId);

        return followUp.ToDto();
    }
}
