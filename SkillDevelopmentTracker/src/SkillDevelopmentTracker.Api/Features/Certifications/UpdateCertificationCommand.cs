using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.Certifications;

public record UpdateCertificationCommand : IRequest<CertificationDto?>
{
    public Guid CertificationId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string IssuingOrganization { get; init; } = string.Empty;
    public DateTime IssueDate { get; init; }
    public DateTime? ExpirationDate { get; init; }
    public string? CredentialId { get; init; }
    public string? CredentialUrl { get; init; }
    public bool IsActive { get; init; }
    public string? Notes { get; init; }
}

public class UpdateCertificationCommandHandler : IRequestHandler<UpdateCertificationCommand, CertificationDto?>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<UpdateCertificationCommandHandler> _logger;

    public UpdateCertificationCommandHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<UpdateCertificationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CertificationDto?> Handle(UpdateCertificationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating certification {CertificationId}", request.CertificationId);

        var certification = await _context.Certifications
            .FirstOrDefaultAsync(c => c.CertificationId == request.CertificationId, cancellationToken);

        if (certification == null)
        {
            _logger.LogWarning("Certification {CertificationId} not found", request.CertificationId);
            return null;
        }

        certification.Name = request.Name;
        certification.IssuingOrganization = request.IssuingOrganization;
        certification.IssueDate = request.IssueDate;
        certification.ExpirationDate = request.ExpirationDate;
        certification.CredentialId = request.CredentialId;
        certification.CredentialUrl = request.CredentialUrl;
        certification.IsActive = request.IsActive;
        certification.Notes = request.Notes;
        certification.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated certification {CertificationId}", request.CertificationId);

        return certification.ToDto();
    }
}
