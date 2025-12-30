using JobSearchOrganizer.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobSearchOrganizer.Api.Features.Companies;

public record CreateCompanyCommand : IRequest<CompanyDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Industry { get; init; }
    public string? Website { get; init; }
    public string? Location { get; init; }
    public string? CompanySize { get; init; }
    public string? CultureNotes { get; init; }
    public string? ResearchNotes { get; init; }
    public bool IsTargetCompany { get; init; }
}

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
    private readonly IJobSearchOrganizerContext _context;
    private readonly ILogger<CreateCompanyCommandHandler> _logger;

    public CreateCompanyCommandHandler(
        IJobSearchOrganizerContext context,
        ILogger<CreateCompanyCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating company for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var company = new Company
        {
            CompanyId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Industry = request.Industry,
            Website = request.Website,
            Location = request.Location,
            CompanySize = request.CompanySize,
            CultureNotes = request.CultureNotes,
            ResearchNotes = request.ResearchNotes,
            IsTargetCompany = request.IsTargetCompany,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Companies.Add(company);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created company {CompanyId} for user {UserId}",
            company.CompanyId,
            request.UserId);

        return company.ToDto();
    }
}
