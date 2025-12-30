using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.Certifications;

public record CreateCertificationCommand : IRequest<CertificationDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string IssuingOrganization { get; init; } = string.Empty;
    public DateTime IssueDate { get; init; }
    public DateTime? ExpirationDate { get; init; }
    public string? CredentialId { get; init; }
    public string? CredentialUrl { get; init; }
    public string? Notes { get; init; }
}

public class CreateCertificationCommandHandler : IRequestHandler<CreateCertificationCommand, CertificationDto>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<CreateCertificationCommandHandler> _logger;

    public CreateCertificationCommandHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<CreateCertificationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CertificationDto> Handle(CreateCertificationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating certification for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var certification = new Certification
        {
            CertificationId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            IssuingOrganization = request.IssuingOrganization,
            IssueDate = request.IssueDate,
            ExpirationDate = request.ExpirationDate,
            CredentialId = request.CredentialId,
            CredentialUrl = request.CredentialUrl,
            IsActive = true,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Certifications.Add(certification);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created certification {CertificationId} for user {UserId}",
            certification.CertificationId,
            request.UserId);

        return certification.ToDto();
    }
}
