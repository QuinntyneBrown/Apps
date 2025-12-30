using PetCareManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.Vaccinations;

public record UpdateVaccinationCommand : IRequest<VaccinationDto?>
{
    public Guid VaccinationId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime DateAdministered { get; init; }
    public DateTime? NextDueDate { get; init; }
    public string? VetName { get; init; }
}

public class UpdateVaccinationCommandHandler : IRequestHandler<UpdateVaccinationCommand, VaccinationDto?>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<UpdateVaccinationCommandHandler> _logger;

    public UpdateVaccinationCommandHandler(
        IPetCareManagerContext context,
        ILogger<UpdateVaccinationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<VaccinationDto?> Handle(UpdateVaccinationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating vaccination {VaccinationId}", request.VaccinationId);

        var vaccination = await _context.Vaccinations
            .FirstOrDefaultAsync(v => v.VaccinationId == request.VaccinationId, cancellationToken);

        if (vaccination == null)
        {
            _logger.LogWarning("Vaccination {VaccinationId} not found", request.VaccinationId);
            return null;
        }

        vaccination.Name = request.Name;
        vaccination.DateAdministered = request.DateAdministered;
        vaccination.NextDueDate = request.NextDueDate;
        vaccination.VetName = request.VetName;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated vaccination {VaccinationId}", request.VaccinationId);

        return vaccination.ToDto();
    }
}
