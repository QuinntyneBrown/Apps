using HomeMaintenanceSchedule.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeMaintenanceSchedule.Api.Features.Contractors;

public record UpdateContractorCommand : IRequest<ContractorDto?>
{
    public Guid ContractorId { get; init; }
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
    public bool IsActive { get; init; }
}

public class UpdateContractorCommandHandler : IRequestHandler<UpdateContractorCommand, ContractorDto?>
{
    private readonly IHomeMaintenanceScheduleContext _context;
    private readonly ILogger<UpdateContractorCommandHandler> _logger;

    public UpdateContractorCommandHandler(
        IHomeMaintenanceScheduleContext context,
        ILogger<UpdateContractorCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ContractorDto?> Handle(UpdateContractorCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating contractor {ContractorId}", request.ContractorId);

        var contractor = await _context.Contractors
            .FirstOrDefaultAsync(c => c.ContractorId == request.ContractorId, cancellationToken);

        if (contractor == null)
        {
            _logger.LogWarning("Contractor {ContractorId} not found", request.ContractorId);
            return null;
        }

        contractor.Name = request.Name;
        contractor.Specialty = request.Specialty;
        contractor.PhoneNumber = request.PhoneNumber;
        contractor.Email = request.Email;
        contractor.Website = request.Website;
        contractor.Address = request.Address;
        contractor.LicenseNumber = request.LicenseNumber;
        contractor.IsInsured = request.IsInsured;
        contractor.Rating = request.Rating;
        contractor.Notes = request.Notes;
        contractor.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated contractor {ContractorId}", request.ContractorId);

        return contractor.ToDto();
    }
}
