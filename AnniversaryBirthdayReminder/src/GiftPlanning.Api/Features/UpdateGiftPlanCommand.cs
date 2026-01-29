using GiftPlanning.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GiftPlanning.Api.Features;

public record UpdateGiftPlanCommand(
    Guid GiftPlanId,
    Guid? CelebrationId,
    string RecipientName,
    string GiftIdea,
    decimal Budget,
    string? Notes,
    bool IsPurchased,
    DateTime? PurchaseDate) : IRequest<GiftPlanDto?>;

public class UpdateGiftPlanCommandHandler : IRequestHandler<UpdateGiftPlanCommand, GiftPlanDto?>
{
    private readonly IGiftPlanningDbContext _context;
    private readonly ILogger<UpdateGiftPlanCommandHandler> _logger;

    public UpdateGiftPlanCommandHandler(IGiftPlanningDbContext context, ILogger<UpdateGiftPlanCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GiftPlanDto?> Handle(UpdateGiftPlanCommand request, CancellationToken cancellationToken)
    {
        var giftPlan = await _context.GiftPlans
            .FirstOrDefaultAsync(g => g.GiftPlanId == request.GiftPlanId, cancellationToken);

        if (giftPlan == null) return null;

        giftPlan.CelebrationId = request.CelebrationId;
        giftPlan.RecipientName = request.RecipientName;
        giftPlan.GiftIdea = request.GiftIdea;
        giftPlan.Budget = request.Budget;
        giftPlan.Notes = request.Notes;
        giftPlan.IsPurchased = request.IsPurchased;
        giftPlan.PurchaseDate = request.PurchaseDate;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Gift plan updated: {GiftPlanId}", giftPlan.GiftPlanId);

        return giftPlan.ToDto();
    }
}
