using GiftPlanning.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GiftPlanning.Api.Features;

public record GetGiftPlansQuery : IRequest<IEnumerable<GiftPlanDto>>;

public class GetGiftPlansQueryHandler : IRequestHandler<GetGiftPlansQuery, IEnumerable<GiftPlanDto>>
{
    private readonly IGiftPlanningDbContext _context;

    public GetGiftPlansQueryHandler(IGiftPlanningDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GiftPlanDto>> Handle(GetGiftPlansQuery request, CancellationToken cancellationToken)
    {
        var giftPlans = await _context.GiftPlans
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return giftPlans.Select(g => g.ToDto());
    }
}
