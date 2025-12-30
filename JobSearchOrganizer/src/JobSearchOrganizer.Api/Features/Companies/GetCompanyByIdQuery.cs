using JobSearchOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobSearchOrganizer.Api.Features.Companies;

public record GetCompanyByIdQuery : IRequest<CompanyDto?>
{
    public Guid CompanyId { get; init; }
}

public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDto?>
{
    private readonly IJobSearchOrganizerContext _context;
    private readonly ILogger<GetCompanyByIdQueryHandler> _logger;

    public GetCompanyByIdQueryHandler(
        IJobSearchOrganizerContext context,
        ILogger<GetCompanyByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CompanyDto?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting company {CompanyId}", request.CompanyId);

        var company = await _context.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CompanyId == request.CompanyId, cancellationToken);

        return company?.ToDto();
    }
}
