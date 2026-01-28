using Intakes.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Intakes.Api.Features;

public record GetIntakeByIdQuery(Guid IntakeId) : IRequest<IntakeDto?>;

public class GetIntakeByIdQueryHandler : IRequestHandler<GetIntakeByIdQuery, IntakeDto?>
{
    private readonly IIntakesDbContext _context;

    public GetIntakeByIdQueryHandler(IIntakesDbContext context)
    {
        _context = context;
    }

    public async Task<IntakeDto?> Handle(GetIntakeByIdQuery request, CancellationToken cancellationToken)
    {
        var intake = await _context.Intakes
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.IntakeId == request.IntakeId, cancellationToken);

        return intake?.ToDto();
    }
}
