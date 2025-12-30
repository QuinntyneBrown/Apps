using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.Certifications;

public record GetCertificationByIdQuery : IRequest<CertificationDto?>
{
    public Guid CertificationId { get; init; }
}

public class GetCertificationByIdQueryHandler : IRequestHandler<GetCertificationByIdQuery, CertificationDto?>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<GetCertificationByIdQueryHandler> _logger;

    public GetCertificationByIdQueryHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<GetCertificationByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CertificationDto?> Handle(GetCertificationByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting certification {CertificationId}", request.CertificationId);

        var certification = await _context.Certifications
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CertificationId == request.CertificationId, cancellationToken);

        if (certification == null)
        {
            _logger.LogWarning("Certification {CertificationId} not found", request.CertificationId);
            return null;
        }

        return certification.ToDto();
    }
}
