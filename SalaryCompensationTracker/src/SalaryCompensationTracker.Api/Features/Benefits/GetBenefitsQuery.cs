using SalaryCompensationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Features.Benefits;

public record GetBenefitsQuery : IRequest<IEnumerable<BenefitDto>>
{
    public Guid? UserId { get; init; }
    public Guid? CompensationId { get; init; }
    public string? Category { get; init; }
}

public class GetBenefitsQueryHandler : IRequestHandler<GetBenefitsQuery, IEnumerable<BenefitDto>>
{
    private readonly ISalaryCompensationTrackerContext _context;
    private readonly ILogger<GetBenefitsQueryHandler> _logger;

    public GetBenefitsQueryHandler(
        ISalaryCompensationTrackerContext context,
        ILogger<GetBenefitsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<BenefitDto>> Handle(GetBenefitsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting benefits for user {UserId}", request.UserId);

        var query = _context.Benefits.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(b => b.UserId == request.UserId.Value);
        }

        if (request.CompensationId.HasValue)
        {
            query = query.Where(b => b.CompensationId == request.CompensationId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Category))
        {
            query = query.Where(b => b.Category == request.Category);
        }

        var benefits = await query
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync(cancellationToken);

        return benefits.Select(b => b.ToDto());
    }
}
