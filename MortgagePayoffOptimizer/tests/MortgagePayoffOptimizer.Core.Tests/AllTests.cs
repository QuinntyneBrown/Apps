// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Core.Tests;

public class MortgageTests
{
    [Test]
    public void ApplyPayment_ValidPayment_ReducesBalance()
    {
        var mortgage = new Mortgage
        {
            MortgageId = Guid.NewGuid(),
            PropertyAddress = "123 Main St",
            Lender = "Test Bank",
            OriginalLoanAmount = 300000m,
            CurrentBalance = 250000m,
            InterestRate = 4.5m,
            LoanTermYears = 30,
            MonthlyPayment = 1520m,
            StartDate = DateTime.UtcNow.AddYears(-5)
        };

        var initialBalance = mortgage.CurrentBalance;
        mortgage.ApplyPayment(1520m);

        Assert.That(mortgage.CurrentBalance, Is.LessThan(initialBalance));
    }

    [Test]
    public void CalculateMonthsRemaining_ValidMortgage_ReturnsPositiveValue()
    {
        var mortgage = new Mortgage
        {
            MortgageId = Guid.NewGuid(),
            CurrentBalance = 200000m,
            InterestRate = 4.0m,
            MonthlyPayment = 1500m
        };

        var months = mortgage.CalculateMonthsRemaining();
        Assert.That(months, Is.GreaterThan(0));
    }

    [Test]
    public void CalculateMonthsRemaining_ZeroBalance_ReturnsZero()
    {
        var mortgage = new Mortgage
        {
            MortgageId = Guid.NewGuid(),
            CurrentBalance = 0m,
            InterestRate = 4.0m,
            MonthlyPayment = 1500m
        };

        Assert.That(mortgage.CalculateMonthsRemaining(), Is.EqualTo(0));
    }

    [Test]
    public void CalculatePayoffDate_ValidMortgage_ReturnsFutureDate()
    {
        var mortgage = new Mortgage
        {
            MortgageId = Guid.NewGuid(),
            PropertyAddress = "123 Main St",
            CurrentBalance = 200000m,
            InterestRate = 4.0m,
            MonthlyPayment = 1500m,
            StartDate = DateTime.UtcNow.AddYears(-5)
        };

        var payoffDate = mortgage.CalculatePayoffDate();
        Assert.That(payoffDate, Is.GreaterThan(DateTime.UtcNow));
    }

    [Test]
    public void MortgageType_CanBeSetToAllValues()
    {
        var mortgage = new Mortgage { MortgageId = Guid.NewGuid(), PropertyAddress = "Test", Lender = "Test" };
        mortgage.MortgageType = MortgageType.Fixed;
        Assert.That(mortgage.MortgageType, Is.EqualTo(MortgageType.Fixed));
        mortgage.MortgageType = MortgageType.ARM;
        Assert.That(mortgage.MortgageType, Is.EqualTo(MortgageType.ARM));
        mortgage.MortgageType = MortgageType.FHA;
        Assert.That(mortgage.MortgageType, Is.EqualTo(MortgageType.FHA));
        mortgage.MortgageType = MortgageType.VA;
        Assert.That(mortgage.MortgageType, Is.EqualTo(MortgageType.VA));
        mortgage.MortgageType = MortgageType.USDA;
        Assert.That(mortgage.MortgageType, Is.EqualTo(MortgageType.USDA));
    }
}

public class PaymentTests
{
    [Test]
    public void CalculateTotalPayment_NoExtraPrincipal_ReturnsBaseAmount()
    {
        var payment = new Payment
        {
            PaymentId = Guid.NewGuid(),
            MortgageId = Guid.NewGuid(),
            PaymentDate = DateTime.UtcNow,
            Amount = 1500m,
            PrincipalAmount = 500m,
            InterestAmount = 1000m,
            ExtraPrincipal = null
        };

        Assert.That(payment.CalculateTotalPayment(), Is.EqualTo(1500m));
    }

    [Test]
    public void CalculateTotalPayment_WithExtraPrincipal_ReturnsTotal()
    {
        var payment = new Payment
        {
            PaymentId = Guid.NewGuid(),
            MortgageId = Guid.NewGuid(),
            PaymentDate = DateTime.UtcNow,
            Amount = 1500m,
            PrincipalAmount = 500m,
            InterestAmount = 1000m,
            ExtraPrincipal = 500m
        };

        Assert.That(payment.CalculateTotalPayment(), Is.EqualTo(2000m));
    }

    [Test]
    public void Constructor_ValidParameters_CreatesPayment()
    {
        var paymentId = Guid.NewGuid();
        var mortgageId = Guid.NewGuid();
        var paymentDate = DateTime.UtcNow;
        var amount = 1500m;
        var principalAmount = 500m;
        var interestAmount = 1000m;
        var extraPrincipal = 200m;

        var payment = new Payment
        {
            PaymentId = paymentId,
            MortgageId = mortgageId,
            PaymentDate = paymentDate,
            Amount = amount,
            PrincipalAmount = principalAmount,
            InterestAmount = interestAmount,
            ExtraPrincipal = extraPrincipal
        };

        Assert.Multiple(() =>
        {
            Assert.That(payment.PaymentId, Is.EqualTo(paymentId));
            Assert.That(payment.MortgageId, Is.EqualTo(mortgageId));
            Assert.That(payment.PaymentDate, Is.EqualTo(paymentDate));
            Assert.That(payment.Amount, Is.EqualTo(amount));
            Assert.That(payment.PrincipalAmount, Is.EqualTo(principalAmount));
            Assert.That(payment.InterestAmount, Is.EqualTo(interestAmount));
            Assert.That(payment.ExtraPrincipal, Is.EqualTo(extraPrincipal));
        });
    }
}

public class RefinanceScenarioTests
{
    [Test]
    public void CalculateBreakEven_PositiveSavings_CalculatesCorrectly()
    {
        var scenario = new RefinanceScenario
        {
            RefinanceScenarioId = Guid.NewGuid(),
            MortgageId = Guid.NewGuid(),
            Name = "Refinance to 3.5%",
            RefinancingCosts = 6000m,
            MonthlySavings = 200m
        };

        scenario.CalculateBreakEven();

        Assert.That(scenario.BreakEvenMonths, Is.EqualTo(30));
    }

    [Test]
    public void CalculateBreakEven_NoSavings_SetsBreakEvenToZero()
    {
        var scenario = new RefinanceScenario
        {
            RefinanceScenarioId = Guid.NewGuid(),
            MortgageId = Guid.NewGuid(),
            Name = "Bad Refinance",
            RefinancingCosts = 6000m,
            MonthlySavings = 0m
        };

        scenario.CalculateBreakEven();

        Assert.That(scenario.BreakEvenMonths, Is.EqualTo(0));
    }

    [Test]
    public void IsRefinancingRecommended_GoodScenario_ReturnsTrue()
    {
        var scenario = new RefinanceScenario
        {
            RefinanceScenarioId = Guid.NewGuid(),
            MortgageId = Guid.NewGuid(),
            Name = "Good Refinance",
            NewLoanTermYears = 30,
            MonthlySavings = 200m,
            BreakEvenMonths = 30
        };

        Assert.That(scenario.IsRefinancingRecommended(), Is.True);
    }

    [Test]
    public void IsRefinancingRecommended_NoSavings_ReturnsFalse()
    {
        var scenario = new RefinanceScenario
        {
            RefinanceScenarioId = Guid.NewGuid(),
            MortgageId = Guid.NewGuid(),
            Name = "Bad Refinance",
            NewLoanTermYears = 30,
            MonthlySavings = 0m,
            BreakEvenMonths = 0
        };

        Assert.That(scenario.IsRefinancingRecommended(), Is.False);
    }

    [Test]
    public void IsRefinancingRecommended_BreakEvenTooLong_ReturnsFalse()
    {
        var scenario = new RefinanceScenario
        {
            RefinanceScenarioId = Guid.NewGuid(),
            MortgageId = Guid.NewGuid(),
            Name = "Long Break-Even",
            NewLoanTermYears = 30,
            MonthlySavings = 100m,
            BreakEvenMonths = 400
        };

        Assert.That(scenario.IsRefinancingRecommended(), Is.False);
    }
}

public class MortgageTypeTests
{
    [Test]
    public void MortgageType_AllValues_HaveCorrectIntValues()
    {
        Assert.That((int)MortgageType.Fixed, Is.EqualTo(0));
        Assert.That((int)MortgageType.ARM, Is.EqualTo(1));
        Assert.That((int)MortgageType.FHA, Is.EqualTo(2));
        Assert.That((int)MortgageType.VA, Is.EqualTo(3));
        Assert.That((int)MortgageType.USDA, Is.EqualTo(4));
    }
}

public class DomainEventsTests
{
    [Test]
    public void MortgageCreatedEvent_ValidParameters_CreatesEvent()
    {
        var evt = new MortgageCreatedEvent
        {
            MortgageId = Guid.NewGuid(),
            PropertyAddress = "123 Main St",
            OriginalLoanAmount = 300000m,
            Timestamp = DateTime.UtcNow
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.PropertyAddress, Is.EqualTo("123 Main St"));
            Assert.That(evt.OriginalLoanAmount, Is.EqualTo(300000m));
        });
    }

    [Test]
    public void PaymentRecordedEvent_ValidParameters_CreatesEvent()
    {
        var evt = new PaymentRecordedEvent
        {
            PaymentId = Guid.NewGuid(),
            MortgageId = Guid.NewGuid(),
            Amount = 1500m,
            PaymentDate = DateTime.UtcNow,
            Timestamp = DateTime.UtcNow
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.Amount, Is.EqualTo(1500m));
            Assert.That(evt.PaymentDate, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ScenarioAnalyzedEvent_ValidParameters_CreatesEvent()
    {
        var evt = new ScenarioAnalyzedEvent
        {
            RefinanceScenarioId = Guid.NewGuid(),
            MortgageId = Guid.NewGuid(),
            MonthlySavings = 200m,
            BreakEvenMonths = 30,
            Timestamp = DateTime.UtcNow
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.MonthlySavings, Is.EqualTo(200m));
            Assert.That(evt.BreakEvenMonths, Is.EqualTo(30));
        });
    }
}
