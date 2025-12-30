using MealPrepPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.MealPlans;

public record GetMealPlansQuery : IRequest<IEnumerable<MealPlanDto>>
{
    public Guid? UserId { get; init; }
    public bool? IsActive { get; init; }
}

public class GetMealPlansQueryHandler : IRequestHandler<GetMealPlansQuery, IEnumerable<MealPlanDto>>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<GetMealPlansQueryHandler> _logger;

    public GetMealPlansQueryHandler(
        IMealPrepPlannerContext context,
        ILogger<GetMealPlansQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MealPlanDto>> Handle(GetMealPlansQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting meal plans for user {UserId}", request.UserId);

        var query = _context.MealPlans.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(m => m.UserId == request.UserId.Value);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(m => m.IsActive == request.IsActive.Value);
        }

        var mealPlans = await query
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync(cancellationToken);

        return mealPlans.Select(m => m.ToDto());
    }
}
