using JobSearchOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobSearchOrganizer.Api.Features.Companies;

public record DeleteCompanyCommand : IRequest<bool>
{
    public Guid CompanyId { get; init; }
}

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, bool>
{
    private readonly IJobSearchOrganizerContext _context;
    private readonly ILogger<DeleteCompanyCommandHandler> _logger;

    public DeleteCompanyCommandHandler(
        IJobSearchOrganizerContext context,
        ILogger<DeleteCompanyCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting company {CompanyId}", request.CompanyId);

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.CompanyId == request.CompanyId, cancellationToken);

        if (company == null)
        {
            _logger.LogWarning("Company {CompanyId} not found", request.CompanyId);
            return false;
        }

        _context.Companies.Remove(company);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted company {CompanyId}", request.CompanyId);

        return true;
    }
}
