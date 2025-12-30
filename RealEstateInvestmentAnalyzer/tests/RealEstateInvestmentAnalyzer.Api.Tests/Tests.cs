using RealEstateInvestmentAnalyzer.Api.Features.Properties;
using RealEstateInvestmentAnalyzer.Api.Features.CashFlows;
using RealEstateInvestmentAnalyzer.Api.Features.Expenses;
using RealEstateInvestmentAnalyzer.Api.Features.Leases;

namespace RealEstateInvestmentAnalyzer.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void PropertyDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var property = new Core.Property
        {
            PropertyId = Guid.NewGuid(),
            Address = "123 Main St",
            PropertyType = Core.PropertyType.SingleFamily,
            PurchasePrice = 250000m,
            PurchaseDate = new DateTime(2020, 1, 1),
            CurrentValue = 300000m,
            SquareFeet = 2000,
            Bedrooms = 3,
            Bathrooms = 2,
            Notes = "Test notes",
        };

        // Act
        var dto = property.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.PropertyId, Is.EqualTo(property.PropertyId));
            Assert.That(dto.Address, Is.EqualTo(property.Address));
            Assert.That(dto.PropertyType, Is.EqualTo(property.PropertyType));
            Assert.That(dto.PurchasePrice, Is.EqualTo(property.PurchasePrice));
            Assert.That(dto.PurchaseDate, Is.EqualTo(property.PurchaseDate));
            Assert.That(dto.CurrentValue, Is.EqualTo(property.CurrentValue));
            Assert.That(dto.SquareFeet, Is.EqualTo(property.SquareFeet));
            Assert.That(dto.Bedrooms, Is.EqualTo(property.Bedrooms));
            Assert.That(dto.Bathrooms, Is.EqualTo(property.Bathrooms));
            Assert.That(dto.Notes, Is.EqualTo(property.Notes));
            Assert.That(dto.Equity, Is.EqualTo(property.CalculateEquity()));
            Assert.That(dto.ROI, Is.EqualTo(property.CalculateROI()));
        });
    }

    [Test]
    public void CashFlowDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var cashFlow = new Core.CashFlow
        {
            CashFlowId = Guid.NewGuid(),
            PropertyId = Guid.NewGuid(),
            Date = new DateTime(2024, 6, 1),
            Income = 2000m,
            Expenses = 500m,
            NetCashFlow = 1500m,
            Notes = "Monthly cash flow",
        };

        // Act
        var dto = cashFlow.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.CashFlowId, Is.EqualTo(cashFlow.CashFlowId));
            Assert.That(dto.PropertyId, Is.EqualTo(cashFlow.PropertyId));
            Assert.That(dto.Date, Is.EqualTo(cashFlow.Date));
            Assert.That(dto.Income, Is.EqualTo(cashFlow.Income));
            Assert.That(dto.Expenses, Is.EqualTo(cashFlow.Expenses));
            Assert.That(dto.NetCashFlow, Is.EqualTo(cashFlow.NetCashFlow));
            Assert.That(dto.Notes, Is.EqualTo(cashFlow.Notes));
        });
    }

    [Test]
    public void ExpenseDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var expense = new Core.Expense
        {
            ExpenseId = Guid.NewGuid(),
            PropertyId = Guid.NewGuid(),
            Description = "Property Tax",
            Amount = 3500m,
            Date = new DateTime(2024, 1, 15),
            Category = "Tax",
            IsRecurring = true,
            Notes = "Annual property tax",
        };

        // Act
        var dto = expense.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ExpenseId, Is.EqualTo(expense.ExpenseId));
            Assert.That(dto.PropertyId, Is.EqualTo(expense.PropertyId));
            Assert.That(dto.Description, Is.EqualTo(expense.Description));
            Assert.That(dto.Amount, Is.EqualTo(expense.Amount));
            Assert.That(dto.Date, Is.EqualTo(expense.Date));
            Assert.That(dto.Category, Is.EqualTo(expense.Category));
            Assert.That(dto.IsRecurring, Is.EqualTo(expense.IsRecurring));
            Assert.That(dto.Notes, Is.EqualTo(expense.Notes));
        });
    }

    [Test]
    public void LeaseDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var lease = new Core.Lease
        {
            LeaseId = Guid.NewGuid(),
            PropertyId = Guid.NewGuid(),
            TenantName = "John Doe",
            MonthlyRent = 1800m,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2024, 12, 31),
            SecurityDeposit = 3600m,
            IsActive = true,
            Notes = "Great tenant",
        };

        // Act
        var dto = lease.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.LeaseId, Is.EqualTo(lease.LeaseId));
            Assert.That(dto.PropertyId, Is.EqualTo(lease.PropertyId));
            Assert.That(dto.TenantName, Is.EqualTo(lease.TenantName));
            Assert.That(dto.MonthlyRent, Is.EqualTo(lease.MonthlyRent));
            Assert.That(dto.StartDate, Is.EqualTo(lease.StartDate));
            Assert.That(dto.EndDate, Is.EqualTo(lease.EndDate));
            Assert.That(dto.SecurityDeposit, Is.EqualTo(lease.SecurityDeposit));
            Assert.That(dto.IsActive, Is.EqualTo(lease.IsActive));
            Assert.That(dto.Notes, Is.EqualTo(lease.Notes));
        });
    }

    [Test]
    public void Property_CalculateEquity_ReturnsCorrectValue()
    {
        // Arrange
        var property = new Core.Property
        {
            PurchasePrice = 250000m,
            CurrentValue = 320000m,
        };

        // Act
        var equity = property.CalculateEquity();

        // Assert
        Assert.That(equity, Is.EqualTo(70000m));
    }

    [Test]
    public void Property_CalculateROI_ReturnsCorrectValue()
    {
        // Arrange
        var property = new Core.Property
        {
            PurchasePrice = 250000m,
            CurrentValue = 320000m,
        };

        // Act
        var roi = property.CalculateROI();

        // Assert
        Assert.That(roi, Is.EqualTo(28m));
    }

    [Test]
    public void Property_CalculateROI_WithZeroPurchasePrice_ReturnsZero()
    {
        // Arrange
        var property = new Core.Property
        {
            PurchasePrice = 0m,
            CurrentValue = 320000m,
        };

        // Act
        var roi = property.CalculateROI();

        // Assert
        Assert.That(roi, Is.EqualTo(0m));
    }

    [Test]
    public void CashFlow_CalculateNetCashFlow_ReturnsCorrectValue()
    {
        // Arrange
        var cashFlow = new Core.CashFlow
        {
            Income = 2000m,
            Expenses = 500m,
        };

        // Act
        cashFlow.CalculateNetCashFlow();

        // Assert
        Assert.That(cashFlow.NetCashFlow, Is.EqualTo(1500m));
    }

    [Test]
    public void Lease_Terminate_SetsIsActiveToFalse()
    {
        // Arrange
        var lease = new Core.Lease
        {
            IsActive = true,
        };

        // Act
        lease.Terminate();

        // Assert
        Assert.That(lease.IsActive, Is.False);
    }
}
