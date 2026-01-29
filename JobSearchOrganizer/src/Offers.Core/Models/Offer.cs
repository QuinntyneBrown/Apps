namespace Offers.Core.Models;

public class Offer
{
    public Guid OfferId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public Guid ApplicationId { get; set; }
    public decimal SalaryAmount { get; set; }
    public string? SalaryType { get; set; }
    public decimal? SigningBonus { get; set; }
    public decimal? AnnualBonus { get; set; }
    public string? Benefits { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public OfferStatus Status { get; set; } = OfferStatus.Pending;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public enum OfferStatus
{
    Pending,
    Accepted,
    Declined,
    Negotiating,
    Expired
}
