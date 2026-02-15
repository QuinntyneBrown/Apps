namespace TaxEstimates.Core.Models;

public class TaxEstimate
{
    public Guid TaxEstimateId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid? BusinessId { get; private set; }
    public int Year { get; private set; }
    public int Quarter { get; private set; }
    public decimal TotalIncome { get; private set; }
    public decimal TotalExpenses { get; private set; }
    public decimal NetIncome { get; private set; }
    public decimal EstimatedTax { get; private set; }
    public decimal TaxRate { get; private set; }
    public DateTime CalculatedAt { get; private set; }

    private TaxEstimate() { }

    public TaxEstimate(Guid tenantId, Guid userId, int year, int quarter, decimal totalIncome, decimal totalExpenses, decimal taxRate, Guid? businessId = null)
    {
        if (year < 2000 || year > 2100)
            throw new ArgumentException("Year must be valid.", nameof(year));
        if (quarter < 1 || quarter > 4)
            throw new ArgumentException("Quarter must be between 1 and 4.", nameof(quarter));
        if (taxRate < 0 || taxRate > 100)
            throw new ArgumentException("Tax rate must be between 0 and 100.", nameof(taxRate));

        TaxEstimateId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        BusinessId = businessId;
        Year = year;
        Quarter = quarter;
        TotalIncome = totalIncome;
        TotalExpenses = totalExpenses;
        NetIncome = totalIncome - totalExpenses;
        TaxRate = taxRate;
        EstimatedTax = NetIncome > 0 ? NetIncome * (taxRate / 100) : 0;
        CalculatedAt = DateTime.UtcNow;
    }

    public void Recalculate(decimal totalIncome, decimal totalExpenses, decimal taxRate)
    {
        TotalIncome = totalIncome;
        TotalExpenses = totalExpenses;
        NetIncome = totalIncome - totalExpenses;
        TaxRate = taxRate;
        EstimatedTax = NetIncome > 0 ? NetIncome * (taxRate / 100) : 0;
        CalculatedAt = DateTime.UtcNow;
    }
}
