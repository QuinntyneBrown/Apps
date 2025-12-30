using SalaryCompensationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Features.Compensations;

public record GetCompensationByIdQuery : IRequest<CompensationDto?>
{
    public Guid CompensationId { get; init; }
}

public class GetCompensationByIdQueryHandler : IRequestHandler<GetCompensationByIdQuery, CompensationDto?>
{
    private readonly ISalaryCompensationTrackerContext _context;
    private readonly ILogger<GetCompensationByIdQueryHandler> _logger;

    public GetCompensationByIdQueryHandler(
        ISalaryCompensationTrackerContext context,
        ILogger<GetCompensationByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CompensationDto?> Handle(GetCompensationByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting compensation {CompensationId}", request.CompensationId);

        var compensation = await _context.Compensations
            .AsNoTracking()
            .Include(c => c.Benefits)
            .FirstOrDefaultAsync(c => c.CompensationId == request.CompensationId, cancellationToken);

        return compensation?.ToDto();
    }
}
