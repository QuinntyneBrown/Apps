using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.ReturnWindows;

public class ReturnWindowDto
{
    public Guid ReturnWindowId { get; set; }
    public Guid PurchaseId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DurationDays { get; set; }
    public ReturnWindowStatus Status { get; set; }
    public string? PolicyDetails { get; set; }
    public string? Conditions { get; set; }
    public decimal? RestockingFeePercent { get; set; }
    public string? Notes { get; set; }
}

public static class ReturnWindowDtoExtensions
{
    public static ReturnWindowDto ToDto(this ReturnWindow returnWindow)
    {
        return new ReturnWindowDto
        {
            ReturnWindowId = returnWindow.ReturnWindowId,
            PurchaseId = returnWindow.PurchaseId,
            StartDate = returnWindow.StartDate,
            EndDate = returnWindow.EndDate,
            DurationDays = returnWindow.DurationDays,
            Status = returnWindow.Status,
            PolicyDetails = returnWindow.PolicyDetails,
            Conditions = returnWindow.Conditions,
            RestockingFeePercent = returnWindow.RestockingFeePercent,
            Notes = returnWindow.Notes
        };
    }
}
