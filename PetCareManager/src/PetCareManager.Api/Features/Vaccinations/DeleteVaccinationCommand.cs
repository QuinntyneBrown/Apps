using PetCareManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.Vaccinations;

public record DeleteVaccinationCommand : IRequest<bool>
{
    public Guid VaccinationId { get; init; }
}

public class DeleteVaccinationCommandHandler : IRequestHandler<DeleteVaccinationCommand, bool>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<DeleteVaccinationCommandHandler> _logger;

    public DeleteVaccinationCommandHandler(
        IPetCareManagerContext context,
        ILogger<DeleteVaccinationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteVaccinationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting vaccination {VaccinationId}", request.VaccinationId);

        var vaccination = await _context.Vaccinations
            .FirstOrDefaultAsync(v => v.VaccinationId == request.VaccinationId, cancellationToken);

        if (vaccination == null)
        {
            _logger.LogWarning("Vaccination {VaccinationId} not found", request.VaccinationId);
            return false;
        }

        _context.Vaccinations.Remove(vaccination);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted vaccination {VaccinationId}", request.VaccinationId);

        return true;
    }
}
