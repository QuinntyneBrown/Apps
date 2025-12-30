using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.FollowUps;

public record DeleteFollowUpCommand : IRequest<bool>
{
    public Guid FollowUpId { get; init; }
}

public class DeleteFollowUpCommandHandler : IRequestHandler<DeleteFollowUpCommand, bool>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<DeleteFollowUpCommandHandler> _logger;

    public DeleteFollowUpCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<DeleteFollowUpCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteFollowUpCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting follow-up {FollowUpId}", request.FollowUpId);

        var followUp = await _context.FollowUps
            .FirstOrDefaultAsync(f => f.FollowUpId == request.FollowUpId, cancellationToken);

        if (followUp == null)
        {
            _logger.LogWarning("Follow-up {FollowUpId} not found", request.FollowUpId);
            return false;
        }

        _context.FollowUps.Remove(followUp);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted follow-up {FollowUpId}", request.FollowUpId);

        return true;
    }
}
