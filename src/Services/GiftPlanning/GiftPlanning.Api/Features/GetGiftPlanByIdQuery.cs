using GiftPlanning.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GiftPlanning.Api.Features;

public record GetGiftPlanByIdQuery(Guid GiftPlanId) : IRequest<GiftPlanDto?>;

public class GetGiftPlanByIdQueryHandler : IRequestHandler<GetGiftPlanByIdQuery, GiftPlanDto?>
{
    private readonly IGiftPlanningDbContext _context;

    public GetGiftPlanByIdQueryHandler(IGiftPlanningDbContext context)
    {
        _context = context;
    }

    public async Task<GiftPlanDto?> Handle(GetGiftPlanByIdQuery request, CancellationToken cancellationToken)
    {
        var giftPlan = await _context.GiftPlans
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.GiftPlanId == request.GiftPlanId, cancellationToken);

        return giftPlan?.ToDto();
    }
}
