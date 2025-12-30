using SalaryCompensationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Features.Benefits;

public record GetBenefitByIdQuery : IRequest<BenefitDto?>
{
    public Guid BenefitId { get; init; }
}

public class GetBenefitByIdQueryHandler : IRequestHandler<GetBenefitByIdQuery, BenefitDto?>
{
    private readonly ISalaryCompensationTrackerContext _context;
    private readonly ILogger<GetBenefitByIdQueryHandler> _logger;

    public GetBenefitByIdQueryHandler(
        ISalaryCompensationTrackerContext context,
        ILogger<GetBenefitByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BenefitDto?> Handle(GetBenefitByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting benefit {BenefitId}", request.BenefitId);

        var benefit = await _context.Benefits
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.BenefitId == request.BenefitId, cancellationToken);

        return benefit?.ToDto();
    }
}
