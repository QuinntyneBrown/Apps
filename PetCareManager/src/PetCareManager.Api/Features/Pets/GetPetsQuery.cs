using PetCareManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.Pets;

public record GetPetsQuery : IRequest<IEnumerable<PetDto>>
{
    public Guid? UserId { get; init; }
    public PetType? PetType { get; init; }
}

public class GetPetsQueryHandler : IRequestHandler<GetPetsQuery, IEnumerable<PetDto>>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<GetPetsQueryHandler> _logger;

    public GetPetsQueryHandler(
        IPetCareManagerContext context,
        ILogger<GetPetsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<PetDto>> Handle(GetPetsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting pets for user {UserId}", request.UserId);

        var query = _context.Pets.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(p => p.UserId == request.UserId.Value);
        }

        if (request.PetType.HasValue)
        {
            query = query.Where(p => p.PetType == request.PetType.Value);
        }

        var pets = await query
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        return pets.Select(p => p.ToDto());
    }
}
