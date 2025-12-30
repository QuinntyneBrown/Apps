using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.Certifications;

public record DeleteCertificationCommand : IRequest<bool>
{
    public Guid CertificationId { get; init; }
}

public class DeleteCertificationCommandHandler : IRequestHandler<DeleteCertificationCommand, bool>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<DeleteCertificationCommandHandler> _logger;

    public DeleteCertificationCommandHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<DeleteCertificationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCertificationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting certification {CertificationId}", request.CertificationId);

        var certification = await _context.Certifications
            .FirstOrDefaultAsync(c => c.CertificationId == request.CertificationId, cancellationToken);

        if (certification == null)
        {
            _logger.LogWarning("Certification {CertificationId} not found", request.CertificationId);
            return false;
        }

        _context.Certifications.Remove(certification);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted certification {CertificationId}", request.CertificationId);

        return true;
    }
}
