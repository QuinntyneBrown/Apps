using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.Certifications;

public record GetCertificationsQuery : IRequest<IEnumerable<CertificationDto>>
{
    public Guid? UserId { get; init; }
    public bool? IsActive { get; init; }
    public string? IssuingOrganization { get; init; }
}

public class GetCertificationsQueryHandler : IRequestHandler<GetCertificationsQuery, IEnumerable<CertificationDto>>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<GetCertificationsQueryHandler> _logger;

    public GetCertificationsQueryHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<GetCertificationsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<CertificationDto>> Handle(GetCertificationsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting certifications for user {UserId}", request.UserId);

        var query = _context.Certifications.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(c => c.UserId == request.UserId.Value);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(c => c.IsActive == request.IsActive.Value);
        }

        if (!string.IsNullOrEmpty(request.IssuingOrganization))
        {
            query = query.Where(c => c.IssuingOrganization == request.IssuingOrganization);
        }

        var certifications = await query
            .OrderByDescending(c => c.IssueDate)
            .ToListAsync(cancellationToken);

        return certifications.Select(c => c.ToDto());
    }
}
