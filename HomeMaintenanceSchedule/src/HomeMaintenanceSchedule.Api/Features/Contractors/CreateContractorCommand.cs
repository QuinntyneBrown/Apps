using HomeMaintenanceSchedule.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeMaintenanceSchedule.Api.Features.Contractors;

public record CreateContractorCommand : IRequest<ContractorDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Specialty { get; init; }
    public string? PhoneNumber { get; init; }
    public string? Email { get; init; }
    public string? Website { get; init; }
    public string? Address { get; init; }
    public string? LicenseNumber { get; init; }
    public bool IsInsured { get; init; }
    public int? Rating { get; init; }
    public string? Notes { get; init; }
}

public class CreateContractorCommandHandler : IRequestHandler<CreateContractorCommand, ContractorDto>
{
    private readonly IHomeMaintenanceScheduleContext _context;
    private readonly ILogger<CreateContractorCommandHandler> _logger;

    public CreateContractorCommandHandler(
        IHomeMaintenanceScheduleContext context,
        ILogger<CreateContractorCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ContractorDto> Handle(CreateContractorCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating contractor for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var contractor = new Contractor
        {
            ContractorId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Specialty = request.Specialty,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Website = request.Website,
            Address = request.Address,
            LicenseNumber = request.LicenseNumber,
            IsInsured = request.IsInsured,
            Rating = request.Rating,
            Notes = request.Notes,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Contractors.Add(contractor);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created contractor {ContractorId} for user {UserId}",
            contractor.ContractorId,
            request.UserId);

        return contractor.ToDto();
    }
}
