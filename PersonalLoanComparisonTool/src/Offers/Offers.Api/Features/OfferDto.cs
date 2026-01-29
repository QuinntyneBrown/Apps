using Offers.Core.Models;

namespace Offers.Api.Features;

public record OfferDto(
    Guid OfferId,
    Guid LoanId,
    string LenderName,
    decimal InterestRate,
    int TermMonths,
    decimal MonthlyPayment,
    decimal TotalCost,
    decimal OriginationFee,
    string? Notes,
    DateTime ReceivedAt);

public static class OfferExtensions
{
    public static OfferDto ToDto(this Offer offer)
    {
        return new OfferDto(
            offer.OfferId,
            offer.LoanId,
            offer.LenderName,
            offer.InterestRate,
            offer.TermMonths,
            offer.MonthlyPayment,
            offer.TotalCost,
            offer.OriginationFee,
            offer.Notes,
            offer.ReceivedAt);
    }
}
