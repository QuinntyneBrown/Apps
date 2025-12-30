using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.FollowUps;

public record UpdateFollowUpCommand : IRequest<FollowUpDto?>
{
    public Guid FollowUpId { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime DueDate { get; init; }
    public string Priority { get; init; } = "Medium";
    public string? Notes { get; init; }
}

public class UpdateFollowUpCommandHandler : IRequestHandler<UpdateFollowUpCommand, FollowUpDto?>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<UpdateFollowUpCommandHandler> _logger;

    public UpdateFollowUpCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<UpdateFollowUpCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<FollowUpDto?> Handle(UpdateFollowUpCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating follow-up {FollowUpId}",
            request.FollowUpId);

        var followUp = await _context.FollowUps
            .FirstOrDefaultAsync(f => f.FollowUpId == request.FollowUpId, cancellationToken);

        if (followUp == null)
        {
            _logger.LogWarning("Follow-up {FollowUpId} not found", request.FollowUpId);
            return null;
        }

        followUp.Description = request.Description;
        followUp.DueDate = request.DueDate;
        followUp.Priority = request.Priority;
        followUp.Notes = request.Notes;
        followUp.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated follow-up {FollowUpId}", request.FollowUpId);

        return followUp.ToDto();
    }
}
