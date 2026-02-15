using Companies.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Companies.Api.Features;

public record DeleteCompanyCommand(Guid CompanyId) : IRequest<bool>;

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, bool>
{
    private readonly ICompaniesDbContext _context;
    private readonly ILogger<DeleteCompanyCommandHandler> _logger;

    public DeleteCompanyCommandHandler(ICompaniesDbContext context, ILogger<DeleteCompanyCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.CompanyId == request.CompanyId, cancellationToken);

        if (company == null) return false;

        _context.Companies.Remove(company);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Company deleted: {CompanyId}", request.CompanyId);

        return true;
    }
}
