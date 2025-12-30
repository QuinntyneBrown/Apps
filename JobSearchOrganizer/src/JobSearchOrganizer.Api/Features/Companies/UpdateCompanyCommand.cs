using JobSearchOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobSearchOrganizer.Api.Features.Companies;

public record UpdateCompanyCommand : IRequest<CompanyDto?>
{
    public Guid CompanyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Industry { get; init; }
    public string? Website { get; init; }
    public string? Location { get; init; }
    public string? CompanySize { get; init; }
    public string? CultureNotes { get; init; }
    public string? ResearchNotes { get; init; }
    public bool IsTargetCompany { get; init; }
}

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyDto?>
{
    private readonly IJobSearchOrganizerContext _context;
    private readonly ILogger<UpdateCompanyCommandHandler> _logger;

    public UpdateCompanyCommandHandler(
        IJobSearchOrganizerContext context,
        ILogger<UpdateCompanyCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CompanyDto?> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating company {CompanyId}", request.CompanyId);

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.CompanyId == request.CompanyId, cancellationToken);

        if (company == null)
        {
            _logger.LogWarning("Company {CompanyId} not found", request.CompanyId);
            return null;
        }

        company.Name = request.Name;
        company.Industry = request.Industry;
        company.Website = request.Website;
        company.Location = request.Location;
        company.CompanySize = request.CompanySize;
        company.CultureNotes = request.CultureNotes;
        company.ResearchNotes = request.ResearchNotes;
        company.IsTargetCompany = request.IsTargetCompany;
        company.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated company {CompanyId}", request.CompanyId);

        return company.ToDto();
    }
}
