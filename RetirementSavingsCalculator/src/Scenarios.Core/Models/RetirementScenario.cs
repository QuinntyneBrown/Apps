namespace Scenarios.Core.Models;

public class RetirementScenario
{
    public Guid RetirementScenarioId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CurrentAge { get; set; }
    public int RetirementAge { get; set; }
    public int LifeExpectancyAge { get; set; }
    public decimal CurrentSavings { get; set; }
    public decimal AnnualContribution { get; set; }
    public decimal ExpectedReturnRate { get; set; }
    public decimal InflationRate { get; set; }
    public decimal ProjectedAnnualIncome { get; set; }
    public decimal ProjectedAnnualExpenses { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    public decimal CalculateProjectedSavings()
    {
        int yearsToRetirement = RetirementAge - CurrentAge;
        if (yearsToRetirement <= 0) return CurrentSavings;

        decimal futureValue = CurrentSavings;
        decimal monthlyReturn = ExpectedReturnRate / 100 / 12;
        decimal monthlyContribution = AnnualContribution / 12;

        for (int i = 0; i < yearsToRetirement * 12; i++)
        {
            futureValue = (futureValue + monthlyContribution) * (1 + monthlyReturn);
        }

        return Math.Round(futureValue, 2);
    }
}
