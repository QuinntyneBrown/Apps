using ProfessionalNetworkCRM.Core;
using ProfessionalNetworkCRM.Core.Models.ReferralAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Referrals;

public record CreateReferralCommand : IRequest<ReferralDto>
{
    public Guid SourceContactId { get; init; }
    public string Description { get; init; } = string.Empty;
    public string? Outcome { get; init; }
    public string? Notes { get; init; }
}

public class CreateReferralCommandHandler : IRequestHandler<CreateReferralCommand, ReferralDto>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<CreateReferralCommandHandler> _logger;
    private readonly ITenantContext _tenantContext;

    public CreateReferralCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<CreateReferralCommandHandler> logger,
        ITenantContext tenantContext)
    {
        _context = context;
        _logger = logger;
        _tenantContext = tenantContext;
    }

    public async Task<ReferralDto> Handle(CreateReferralCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating referral from contact {SourceContactId}",
            request.SourceContactId);

        var referral = new Referral
        {
            ReferralId = Guid.NewGuid(),
            SourceContactId = request.SourceContactId,
            TenantId = _tenantContext.TenantId,
            Description = request.Description,
            Outcome = request.Outcome,
            Notes = request.Notes,
            ThankYouSent = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Referrals.Add(referral);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created referral {ReferralId} from contact {SourceContactId}",
            referral.ReferralId,
            request.SourceContactId);

        return referral.ToDto();
    }
}
