using Offers.Core.Models;

namespace Offers.Api.Features;

public record OfferDto(
    Guid OfferId,
    Guid UserId,
    Guid ApplicationId,
    decimal SalaryAmount,
    string? SalaryType,
    decimal? SigningBonus,
    decimal? AnnualBonus,
    string? Benefits,
    DateTime StartDate,
    DateTime? ExpirationDate,
    OfferStatus Status,
    string? Notes,
    DateTime CreatedAt);

public static class OfferExtensions
{
    public static OfferDto ToDto(this Offer offer)
    {
        return new OfferDto(
            offer.OfferId,
            offer.UserId,
            offer.ApplicationId,
            offer.SalaryAmount,
            offer.SalaryType,
            offer.SigningBonus,
            offer.AnnualBonus,
            offer.Benefits,
            offer.StartDate,
            offer.ExpirationDate,
            offer.Status,
            offer.Notes,
            offer.CreatedAt);
    }
}
