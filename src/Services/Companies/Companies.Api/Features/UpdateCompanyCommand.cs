using Companies.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Companies.Api.Features;

public record UpdateCompanyCommand(
    Guid CompanyId,
    string Name,
    string? Website,
    string? Industry,
    string? Location,
    string? Size,
    string? Description,
    string? Notes,
    int? Rating) : IRequest<CompanyDto?>;

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyDto?>
{
    private readonly ICompaniesDbContext _context;
    private readonly ILogger<UpdateCompanyCommandHandler> _logger;

    public UpdateCompanyCommandHandler(ICompaniesDbContext context, ILogger<UpdateCompanyCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CompanyDto?> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.CompanyId == request.CompanyId, cancellationToken);

        if (company == null) return null;

        company.Name = request.Name;
        company.Website = request.Website;
        company.Industry = request.Industry;
        company.Location = request.Location;
        company.Size = request.Size;
        company.Description = request.Description;
        company.Notes = request.Notes;
        company.Rating = request.Rating;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Company updated: {CompanyId}", company.CompanyId);

        return company.ToDto();
    }
}
