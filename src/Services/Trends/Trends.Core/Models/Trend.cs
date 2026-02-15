namespace Trends.Core.Models;

public class Trend
{
    public Guid TrendId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string TrendType { get; private set; } = string.Empty;
    public string Summary { get; private set; } = string.Empty;
    public decimal AverageSystolic { get; private set; }
    public decimal AverageDiastolic { get; private set; }
    public decimal AveragePulse { get; private set; }
    public DateTime PeriodStart { get; private set; }
    public DateTime PeriodEnd { get; private set; }
    public DateTime AnalyzedAt { get; private set; }

    private Trend() { }

    public Trend(Guid tenantId, Guid userId, string trendType, string summary,
        decimal averageSystolic, decimal averageDiastolic, decimal averagePulse,
        DateTime periodStart, DateTime periodEnd)
    {
        if (string.IsNullOrWhiteSpace(trendType))
            throw new ArgumentException("Trend type cannot be empty.", nameof(trendType));
        if (string.IsNullOrWhiteSpace(summary))
            throw new ArgumentException("Summary cannot be empty.", nameof(summary));

        TrendId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        TrendType = trendType;
        Summary = summary;
        AverageSystolic = averageSystolic;
        AverageDiastolic = averageDiastolic;
        AveragePulse = averagePulse;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
        AnalyzedAt = DateTime.UtcNow;
    }

    public void UpdateSummary(string summary)
    {
        if (string.IsNullOrWhiteSpace(summary))
            throw new ArgumentException("Summary cannot be empty.", nameof(summary));
        Summary = summary;
    }
}
