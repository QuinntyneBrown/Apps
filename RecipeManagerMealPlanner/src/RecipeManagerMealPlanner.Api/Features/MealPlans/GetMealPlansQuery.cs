using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.MealPlans;

public record GetMealPlansQuery : IRequest<IEnumerable<MealPlanDto>>
{
    public Guid? UserId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string? MealType { get; init; }
    public bool? PreparedOnly { get; init; }
}

public class GetMealPlansQueryHandler : IRequestHandler<GetMealPlansQuery, IEnumerable<MealPlanDto>>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<GetMealPlansQueryHandler> _logger;

    public GetMealPlansQueryHandler(
        IRecipeManagerMealPlannerContext context,
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

        if (request.StartDate.HasValue)
        {
            query = query.Where(m => m.MealDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(m => m.MealDate <= request.EndDate.Value);
        }

        if (!string.IsNullOrEmpty(request.MealType))
        {
            query = query.Where(m => m.MealType == request.MealType);
        }

        if (request.PreparedOnly == true)
        {
            query = query.Where(m => m.IsPrepared);
        }

        var mealPlans = await query
            .OrderBy(m => m.MealDate)
            .ToListAsync(cancellationToken);

        return mealPlans.Select(m => m.ToDto());
    }
}
