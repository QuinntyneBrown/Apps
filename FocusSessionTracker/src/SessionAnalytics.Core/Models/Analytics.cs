namespace SessionAnalytics.Core.Models;

public class Analytics
{
    public Guid AnalyticsId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime Date { get; private set; }
    public int TotalSessions { get; private set; }
    public int TotalFocusMinutes { get; private set; }
    public int TotalDistractions { get; private set; }
    public double AverageSessionLength { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Analytics() { }

    public Analytics(Guid tenantId, Guid userId, DateTime date)
    {
        AnalyticsId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Date = date.Date;
        TotalSessions = 0;
        TotalFocusMinutes = 0;
        TotalDistractions = 0;
        AverageSessionLength = 0;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddSession(int focusMinutes)
    {
        TotalSessions++;
        TotalFocusMinutes += focusMinutes;
        AverageSessionLength = TotalSessions > 0 ? (double)TotalFocusMinutes / TotalSessions : 0;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddDistraction()
    {
        TotalDistractions++;
        UpdatedAt = DateTime.UtcNow;
    }
}
