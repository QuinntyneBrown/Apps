using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Referrals;

public record GetReferralsQuery : IRequest<IEnumerable<ReferralDto>>
{
    public Guid? SourceContactId { get; init; }
    public bool? ThankYouSent { get; init; }
}

public class GetReferralsQueryHandler : IRequestHandler<GetReferralsQuery, IEnumerable<ReferralDto>>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<GetReferralsQueryHandler> _logger;

    public GetReferralsQueryHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<GetReferralsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ReferralDto>> Handle(GetReferralsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting referrals with filters: SourceContactId={SourceContactId}, ThankYouSent={ThankYouSent}",
            request.SourceContactId, request.ThankYouSent);

        var query = _context.Referrals.AsQueryable();

        if (request.SourceContactId.HasValue)
        {
            query = query.Where(r => r.SourceContactId == request.SourceContactId.Value);
        }

        if (request.ThankYouSent.HasValue)
        {
            query = query.Where(r => r.ThankYouSent == request.ThankYouSent.Value);
        }

        var referrals = await query
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Found {Count} referrals", referrals.Count);

        return referrals.Select(r => r.ToDto());
    }
}
