using PetCareManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.Pets;

public record GetPetByIdQuery : IRequest<PetDto?>
{
    public Guid PetId { get; init; }
}

public class GetPetByIdQueryHandler : IRequestHandler<GetPetByIdQuery, PetDto?>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<GetPetByIdQueryHandler> _logger;

    public GetPetByIdQueryHandler(
        IPetCareManagerContext context,
        ILogger<GetPetByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PetDto?> Handle(GetPetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting pet {PetId}", request.PetId);

        var pet = await _context.Pets
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PetId == request.PetId, cancellationToken);

        return pet?.ToDto();
    }
}
