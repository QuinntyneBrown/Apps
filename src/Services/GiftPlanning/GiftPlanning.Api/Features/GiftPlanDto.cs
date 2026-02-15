using GiftPlanning.Core.Models;

namespace GiftPlanning.Api.Features;

public record GiftPlanDto(
    Guid GiftPlanId,
    Guid UserId,
    Guid? CelebrationId,
    string RecipientName,
    string GiftIdea,
    decimal Budget,
    string? Notes,
    bool IsPurchased,
    DateTime? PurchaseDate,
    DateTime CreatedAt);

public static class GiftPlanExtensions
{
    public static GiftPlanDto ToDto(this GiftPlan giftPlan)
    {
        return new GiftPlanDto(
            giftPlan.GiftPlanId,
            giftPlan.UserId,
            giftPlan.CelebrationId,
            giftPlan.RecipientName,
            giftPlan.GiftIdea,
            giftPlan.Budget,
            giftPlan.Notes,
            giftPlan.IsPurchased,
            giftPlan.PurchaseDate,
            giftPlan.CreatedAt);
    }
}
