using ProfessionalNetworkCRM.Core;
using ProfessionalNetworkCRM.Core.Models.IntroductionAggregate.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Introductions;

public record GetIntroductionsQuery : IRequest<IEnumerable<IntroductionDto>>
{
    public Guid? ContactId { get; init; }
    public IntroductionStatus? Status { get; init; }
}

public class GetIntroductionsQueryHandler : IRequestHandler<GetIntroductionsQuery, IEnumerable<IntroductionDto>>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<GetIntroductionsQueryHandler> _logger;

    public GetIntroductionsQueryHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<GetIntroductionsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<IntroductionDto>> Handle(GetIntroductionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting introductions with filters: ContactId={ContactId}, Status={Status}",
            request.ContactId, request.Status);

        var query = _context.Introductions.AsQueryable();

        if (request.ContactId.HasValue)
        {
            query = query.Where(i => i.FromContactId == request.ContactId.Value || i.ToContactId == request.ContactId.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(i => i.Status == request.Status.Value);
        }

        var introductions = await query
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Found {Count} introductions", introductions.Count);

        return introductions.Select(i => i.ToDto());
    }
}
