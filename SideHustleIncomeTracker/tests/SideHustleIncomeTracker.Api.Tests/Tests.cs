using SideHustleIncomeTracker.Api.Features.Businesses;
using SideHustleIncomeTracker.Api.Features.Incomes;
using SideHustleIncomeTracker.Api.Features.Expenses;
using SideHustleIncomeTracker.Api.Features.TaxEstimates;

namespace SideHustleIncomeTracker.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void BusinessDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var business = new Core.Business
        {
            BusinessId = Guid.NewGuid(),
            Name = "Test Business",
            Description = "Test Description",
            StartDate = new DateTime(2023, 1, 1),
            IsActive = true,
            TaxId = "12-3456789",
            Notes = "Test Notes",
            TotalIncome = 50000m,
            TotalExpenses = 10000m,
        };

        // Act
        var dto = business.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.BusinessId, Is.EqualTo(business.BusinessId));
            Assert.That(dto.Name, Is.EqualTo(business.Name));
            Assert.That(dto.Description, Is.EqualTo(business.Description));
            Assert.That(dto.StartDate, Is.EqualTo(business.StartDate));
            Assert.That(dto.IsActive, Is.EqualTo(business.IsActive));
            Assert.That(dto.TaxId, Is.EqualTo(business.TaxId));
            Assert.That(dto.Notes, Is.EqualTo(business.Notes));
            Assert.That(dto.TotalIncome, Is.EqualTo(business.TotalIncome));
            Assert.That(dto.TotalExpenses, Is.EqualTo(business.TotalExpenses));
            Assert.That(dto.NetProfit, Is.EqualTo(business.CalculateNetProfit()));
        });
    }

    [Test]
    public void IncomeDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var income = new Core.Income
        {
            IncomeId = Guid.NewGuid(),
            BusinessId = Guid.NewGuid(),
            Description = "Web Development Project",
            Amount = 5000m,
            IncomeDate = new DateTime(2024, 1, 15),
            Client = "ABC Corp",
            InvoiceNumber = "INV-001",
            IsPaid = true,
            Notes = "Paid on time",
        };

        // Act
        var dto = income.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.IncomeId, Is.EqualTo(income.IncomeId));
            Assert.That(dto.BusinessId, Is.EqualTo(income.BusinessId));
            Assert.That(dto.Description, Is.EqualTo(income.Description));
            Assert.That(dto.Amount, Is.EqualTo(income.Amount));
            Assert.That(dto.IncomeDate, Is.EqualTo(income.IncomeDate));
            Assert.That(dto.Client, Is.EqualTo(income.Client));
            Assert.That(dto.InvoiceNumber, Is.EqualTo(income.InvoiceNumber));
            Assert.That(dto.IsPaid, Is.EqualTo(income.IsPaid));
            Assert.That(dto.Notes, Is.EqualTo(income.Notes));
        });
    }

    [Test]
    public void ExpenseDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var expense = new Core.Expense
        {
            ExpenseId = Guid.NewGuid(),
            BusinessId = Guid.NewGuid(),
            Description = "Software Subscription",
            Amount = 99.99m,
            ExpenseDate = new DateTime(2024, 1, 1),
            Category = "Software",
            Vendor = "Adobe",
            IsTaxDeductible = true,
            Notes = "Annual subscription",
        };

        // Act
        var dto = expense.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ExpenseId, Is.EqualTo(expense.ExpenseId));
            Assert.That(dto.BusinessId, Is.EqualTo(expense.BusinessId));
            Assert.That(dto.Description, Is.EqualTo(expense.Description));
            Assert.That(dto.Amount, Is.EqualTo(expense.Amount));
            Assert.That(dto.ExpenseDate, Is.EqualTo(expense.ExpenseDate));
            Assert.That(dto.Category, Is.EqualTo(expense.Category));
            Assert.That(dto.Vendor, Is.EqualTo(expense.Vendor));
            Assert.That(dto.IsTaxDeductible, Is.EqualTo(expense.IsTaxDeductible));
            Assert.That(dto.Notes, Is.EqualTo(expense.Notes));
        });
    }

    [Test]
    public void TaxEstimateDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var taxEstimate = new Core.TaxEstimate
        {
            TaxEstimateId = Guid.NewGuid(),
            BusinessId = Guid.NewGuid(),
            TaxYear = 2024,
            Quarter = 1,
            NetProfit = 10000m,
            SelfEmploymentTax = 1413m,
            IncomeTax = 2200m,
            TotalEstimatedTax = 3613m,
            IsPaid = true,
            PaymentDate = new DateTime(2024, 4, 15),
        };

        // Act
        var dto = taxEstimate.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.TaxEstimateId, Is.EqualTo(taxEstimate.TaxEstimateId));
            Assert.That(dto.BusinessId, Is.EqualTo(taxEstimate.BusinessId));
            Assert.That(dto.TaxYear, Is.EqualTo(taxEstimate.TaxYear));
            Assert.That(dto.Quarter, Is.EqualTo(taxEstimate.Quarter));
            Assert.That(dto.NetProfit, Is.EqualTo(taxEstimate.NetProfit));
            Assert.That(dto.SelfEmploymentTax, Is.EqualTo(taxEstimate.SelfEmploymentTax));
            Assert.That(dto.IncomeTax, Is.EqualTo(taxEstimate.IncomeTax));
            Assert.That(dto.TotalEstimatedTax, Is.EqualTo(taxEstimate.TotalEstimatedTax));
            Assert.That(dto.IsPaid, Is.EqualTo(taxEstimate.IsPaid));
            Assert.That(dto.PaymentDate, Is.EqualTo(taxEstimate.PaymentDate));
        });
    }

    [Test]
    public void Business_CalculateNetProfit_ReturnsCorrectValue()
    {
        // Arrange
        var business = new Core.Business
        {
            BusinessId = Guid.NewGuid(),
            Name = "Test Business",
            TotalIncome = 50000m,
            TotalExpenses = 10000m,
        };

        // Act
        var netProfit = business.CalculateNetProfit();

        // Assert
        Assert.That(netProfit, Is.EqualTo(40000m));
    }

    [Test]
    public void TaxEstimate_CalculateTotalTax_ReturnsCorrectValue()
    {
        // Arrange
        var taxEstimate = new Core.TaxEstimate
        {
            TaxEstimateId = Guid.NewGuid(),
            BusinessId = Guid.NewGuid(),
            TaxYear = 2024,
            Quarter = 1,
            NetProfit = 10000m,
            SelfEmploymentTax = 1413m,
            IncomeTax = 2200m,
        };

        // Act
        taxEstimate.CalculateTotalTax();

        // Assert
        Assert.That(taxEstimate.TotalEstimatedTax, Is.EqualTo(3613m));
    }
}
