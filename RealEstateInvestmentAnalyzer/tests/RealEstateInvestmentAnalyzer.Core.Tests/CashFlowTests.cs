// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Core.Tests;

public class CashFlowTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesCashFlow()
    {
        // Arrange & Act
        var cashFlow = new CashFlow();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(cashFlow.CashFlowId, Is.EqualTo(Guid.Empty));
            Assert.That(cashFlow.PropertyId, Is.EqualTo(Guid.Empty));
            Assert.That(cashFlow.Date, Is.EqualTo(default(DateTime)));
            Assert.That(cashFlow.Income, Is.EqualTo(0m));
            Assert.That(cashFlow.Expenses, Is.EqualTo(0m));
            Assert.That(cashFlow.NetCashFlow, Is.EqualTo(0m));
            Assert.That(cashFlow.Notes, Is.Null);
            Assert.That(cashFlow.Property, Is.Null);
        });
    }

    [Test]
    public void Constructor_WithValidParameters_CreatesCashFlow()
    {
        // Arrange
        var cashFlowId = Guid.NewGuid();
        var propertyId = Guid.NewGuid();
        var date = new DateTime(2024, 6, 1);
        var income = 1500m;
        var expenses = 800m;

        // Act
        var cashFlow = new CashFlow
        {
            CashFlowId = cashFlowId,
            PropertyId = propertyId,
            Date = date,
            Income = income,
            Expenses = expenses
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(cashFlow.CashFlowId, Is.EqualTo(cashFlowId));
            Assert.That(cashFlow.PropertyId, Is.EqualTo(propertyId));
            Assert.That(cashFlow.Date, Is.EqualTo(date));
            Assert.That(cashFlow.Income, Is.EqualTo(income));
            Assert.That(cashFlow.Expenses, Is.EqualTo(expenses));
        });
    }

    [Test]
    public void CalculateNetCashFlow_PositiveCashFlow_CalculatesCorrectly()
    {
        // Arrange
        var cashFlow = new CashFlow
        {
            Income = 2000m,
            Expenses = 1200m
        };

        // Act
        cashFlow.CalculateNetCashFlow();

        // Assert
        Assert.That(cashFlow.NetCashFlow, Is.EqualTo(800m));
    }

    [Test]
    public void CalculateNetCashFlow_NegativeCashFlow_CalculatesCorrectly()
    {
        // Arrange
        var cashFlow = new CashFlow
        {
            Income = 1000m,
            Expenses = 1500m
        };

        // Act
        cashFlow.CalculateNetCashFlow();

        // Assert
        Assert.That(cashFlow.NetCashFlow, Is.EqualTo(-500m));
    }

    [Test]
    public void CalculateNetCashFlow_ZeroCashFlow_CalculatesCorrectly()
    {
        // Arrange
        var cashFlow = new CashFlow
        {
            Income = 1500m,
            Expenses = 1500m
        };

        // Act
        cashFlow.CalculateNetCashFlow();

        // Assert
        Assert.That(cashFlow.NetCashFlow, Is.EqualTo(0m));
    }

    [Test]
    public void CalculateNetCashFlow_NoIncome_CalculatesNegative()
    {
        // Arrange
        var cashFlow = new CashFlow
        {
            Income = 0m,
            Expenses = 800m
        };

        // Act
        cashFlow.CalculateNetCashFlow();

        // Assert
        Assert.That(cashFlow.NetCashFlow, Is.EqualTo(-800m));
    }

    [Test]
    public void CalculateNetCashFlow_NoExpenses_CalculatesPositive()
    {
        // Arrange
        var cashFlow = new CashFlow
        {
            Income = 1500m,
            Expenses = 0m
        };

        // Act
        cashFlow.CalculateNetCashFlow();

        // Assert
        Assert.That(cashFlow.NetCashFlow, Is.EqualTo(1500m));
    }

    [Test]
    public void CashFlow_HighIncome_StoresCorrectly()
    {
        // Arrange & Act
        var cashFlow = new CashFlow
        {
            Income = 10000m,
            Expenses = 3000m
        };
        cashFlow.CalculateNetCashFlow();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(cashFlow.Income, Is.EqualTo(10000m));
            Assert.That(cashFlow.NetCashFlow, Is.EqualTo(7000m));
        });
    }

    [Test]
    public void CashFlow_WithNotes_SetsCorrectly()
    {
        // Arrange
        var notes = "Rental income from tenant plus parking fees";

        // Act
        var cashFlow = new CashFlow
        {
            Income = 1600m,
            Expenses = 900m,
            Notes = notes
        };

        // Assert
        Assert.That(cashFlow.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void CashFlow_MonthlyRecord_StoresCorrectly()
    {
        // Arrange & Act
        var cashFlow = new CashFlow
        {
            Date = new DateTime(2024, 1, 31),
            Income = 2500m,
            Expenses = 1800m
        };
        cashFlow.CalculateNetCashFlow();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(cashFlow.Date, Is.EqualTo(new DateTime(2024, 1, 31)));
            Assert.That(cashFlow.NetCashFlow, Is.EqualTo(700m));
        });
    }

    [Test]
    public void CashFlow_WithAllProperties_SetsAllCorrectly()
    {
        // Arrange & Act
        var cashFlow = new CashFlow
        {
            CashFlowId = Guid.NewGuid(),
            PropertyId = Guid.NewGuid(),
            Date = new DateTime(2024, 5, 31),
            Income = 2200m,
            Expenses = 1300m,
            Notes = "Monthly cash flow for May 2024"
        };
        cashFlow.CalculateNetCashFlow();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(cashFlow.Date, Is.EqualTo(new DateTime(2024, 5, 31)));
            Assert.That(cashFlow.Income, Is.EqualTo(2200m));
            Assert.That(cashFlow.Expenses, Is.EqualTo(1300m));
            Assert.That(cashFlow.NetCashFlow, Is.EqualTo(900m));
            Assert.That(cashFlow.Notes, Is.EqualTo("Monthly cash flow for May 2024"));
        });
    }

    [Test]
    public void CalculateNetCashFlow_DecimalPrecision_CalculatesCorrectly()
    {
        // Arrange
        var cashFlow = new CashFlow
        {
            Income = 1523.75m,
            Expenses = 987.25m
        };

        // Act
        cashFlow.CalculateNetCashFlow();

        // Assert
        Assert.That(cashFlow.NetCashFlow, Is.EqualTo(536.50m));
    }

    [Test]
    public void CalculateNetCashFlow_LargeValues_CalculatesCorrectly()
    {
        // Arrange
        var cashFlow = new CashFlow
        {
            Income = 50000m,
            Expenses = 35000m
        };

        // Act
        cashFlow.CalculateNetCashFlow();

        // Assert
        Assert.That(cashFlow.NetCashFlow, Is.EqualTo(15000m));
    }
}
