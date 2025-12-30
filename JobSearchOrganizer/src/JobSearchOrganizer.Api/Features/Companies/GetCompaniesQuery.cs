using JobSearchOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobSearchOrganizer.Api.Features.Companies;

public record GetCompaniesQuery : IRequest<IEnumerable<CompanyDto>>
{
    public Guid? UserId { get; init; }
    public string? Industry { get; init; }
    public bool? IsTargetCompany { get; init; }
}

public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, IEnumerable<CompanyDto>>
{
    private readonly IJobSearchOrganizerContext _context;
    private readonly ILogger<GetCompaniesQueryHandler> _logger;

    public GetCompaniesQueryHandler(
        IJobSearchOrganizerContext context,
        ILogger<GetCompaniesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<CompanyDto>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting companies for user {UserId}", request.UserId);

        var query = _context.Companies.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(c => c.UserId == request.UserId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Industry))
        {
            query = query.Where(c => c.Industry == request.Industry);
        }

        if (request.IsTargetCompany.HasValue)
        {
            query = query.Where(c => c.IsTargetCompany == request.IsTargetCompany.Value);
        }

        var companies = await query
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);

        return companies.Select(c => c.ToDto());
    }
}
