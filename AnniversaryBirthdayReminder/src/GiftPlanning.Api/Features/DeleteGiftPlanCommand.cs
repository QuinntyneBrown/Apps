using GiftPlanning.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GiftPlanning.Api.Features;

public record DeleteGiftPlanCommand(Guid GiftPlanId) : IRequest<bool>;

public class DeleteGiftPlanCommandHandler : IRequestHandler<DeleteGiftPlanCommand, bool>
{
    private readonly IGiftPlanningDbContext _context;
    private readonly ILogger<DeleteGiftPlanCommandHandler> _logger;

    public DeleteGiftPlanCommandHandler(IGiftPlanningDbContext context, ILogger<DeleteGiftPlanCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteGiftPlanCommand request, CancellationToken cancellationToken)
    {
        var giftPlan = await _context.GiftPlans
            .FirstOrDefaultAsync(g => g.GiftPlanId == request.GiftPlanId, cancellationToken);

        if (giftPlan == null) return false;

        _context.GiftPlans.Remove(giftPlan);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Gift plan deleted: {GiftPlanId}", request.GiftPlanId);
        return true;
    }
}
