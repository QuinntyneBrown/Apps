using PetCareManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.Vaccinations;

public record CreateVaccinationCommand : IRequest<VaccinationDto>
{
    public Guid PetId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime DateAdministered { get; init; }
    public DateTime? NextDueDate { get; init; }
    public string? VetName { get; init; }
}

public class CreateVaccinationCommandHandler : IRequestHandler<CreateVaccinationCommand, VaccinationDto>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<CreateVaccinationCommandHandler> _logger;

    public CreateVaccinationCommandHandler(
        IPetCareManagerContext context,
        ILogger<CreateVaccinationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<VaccinationDto> Handle(CreateVaccinationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating vaccination for pet {PetId}, name: {Name}",
            request.PetId,
            request.Name);

        var vaccination = new Vaccination
        {
            VaccinationId = Guid.NewGuid(),
            PetId = request.PetId,
            Name = request.Name,
            DateAdministered = request.DateAdministered,
            NextDueDate = request.NextDueDate,
            VetName = request.VetName,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Vaccinations.Add(vaccination);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created vaccination {VaccinationId} for pet {PetId}",
            vaccination.VaccinationId,
            request.PetId);

        return vaccination.ToDto();
    }
}
