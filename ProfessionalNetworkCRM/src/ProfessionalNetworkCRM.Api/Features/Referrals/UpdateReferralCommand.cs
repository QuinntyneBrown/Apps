using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Referrals;

public record UpdateReferralCommand : IRequest<ReferralDto?>
{
    public Guid ReferralId { get; init; }
    public string? Outcome { get; init; }
    public string? Notes { get; init; }
    public bool? ThankYouSent { get; init; }
}

public class UpdateReferralCommandHandler : IRequestHandler<UpdateReferralCommand, ReferralDto?>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<UpdateReferralCommandHandler> _logger;

    public UpdateReferralCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<UpdateReferralCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReferralDto?> Handle(UpdateReferralCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating referral {ReferralId}", request.ReferralId);

        var referral = await _context.Referrals
            .FirstOrDefaultAsync(r => r.ReferralId == request.ReferralId, cancellationToken);

        if (referral == null)
        {
            _logger.LogWarning("Referral {ReferralId} not found", request.ReferralId);
            return null;
        }

        if (!string.IsNullOrEmpty(request.Outcome))
        {
            referral.UpdateOutcome(request.Outcome);
        }
        else if (!string.IsNullOrEmpty(request.Notes))
        {
            // Only update notes if outcome is not being updated
            referral.Notes = request.Notes;
            referral.UpdatedAt = DateTime.UtcNow;
        }

        if (request.ThankYouSent.HasValue)
        {
            if (request.ThankYouSent.Value)
            {
                referral.MarkThankYouSent();
            }
            else
            {
                referral.ThankYouSent = false;
                referral.UpdatedAt = DateTime.UtcNow;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated referral {ReferralId}", request.ReferralId);

        return referral.ToDto();
    }
}
