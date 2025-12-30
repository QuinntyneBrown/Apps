// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HouseholdBudgetManager.Core.Tests;

public class ExpenseCategoryTests
{
    [Test]
    public void ExpenseCategory_HousingValue_EqualsZero()
    {
        // Assert
        Assert.That((int)ExpenseCategory.Housing, Is.EqualTo(0));
    }

    [Test]
    public void ExpenseCategory_TransportationValue_EqualsOne()
    {
        // Assert
        Assert.That((int)ExpenseCategory.Transportation, Is.EqualTo(1));
    }

    [Test]
    public void ExpenseCategory_FoodValue_EqualsTwo()
    {
        // Assert
        Assert.That((int)ExpenseCategory.Food, Is.EqualTo(2));
    }

    [Test]
    public void ExpenseCategory_HealthcareValue_EqualsThree()
    {
        // Assert
        Assert.That((int)ExpenseCategory.Healthcare, Is.EqualTo(3));
    }

    [Test]
    public void ExpenseCategory_EntertainmentValue_EqualsFour()
    {
        // Assert
        Assert.That((int)ExpenseCategory.Entertainment, Is.EqualTo(4));
    }

    [Test]
    public void ExpenseCategory_PersonalCareValue_EqualsFive()
    {
        // Assert
        Assert.That((int)ExpenseCategory.PersonalCare, Is.EqualTo(5));
    }

    [Test]
    public void ExpenseCategory_EducationValue_EqualsSix()
    {
        // Assert
        Assert.That((int)ExpenseCategory.Education, Is.EqualTo(6));
    }

    [Test]
    public void ExpenseCategory_DebtPaymentValue_EqualsSeven()
    {
        // Assert
        Assert.That((int)ExpenseCategory.DebtPayment, Is.EqualTo(7));
    }

    [Test]
    public void ExpenseCategory_SavingsValue_EqualsEight()
    {
        // Assert
        Assert.That((int)ExpenseCategory.Savings, Is.EqualTo(8));
    }

    [Test]
    public void ExpenseCategory_OtherValue_EqualsNine()
    {
        // Assert
        Assert.That((int)ExpenseCategory.Other, Is.EqualTo(9));
    }

    [Test]
    public void ExpenseCategory_AllValues_CanBeAssigned()
    {
        // Act & Assert
        ExpenseCategory category;

        Assert.DoesNotThrow(() => category = ExpenseCategory.Housing);
        Assert.DoesNotThrow(() => category = ExpenseCategory.Transportation);
        Assert.DoesNotThrow(() => category = ExpenseCategory.Food);
        Assert.DoesNotThrow(() => category = ExpenseCategory.Healthcare);
        Assert.DoesNotThrow(() => category = ExpenseCategory.Entertainment);
        Assert.DoesNotThrow(() => category = ExpenseCategory.PersonalCare);
        Assert.DoesNotThrow(() => category = ExpenseCategory.Education);
        Assert.DoesNotThrow(() => category = ExpenseCategory.DebtPayment);
        Assert.DoesNotThrow(() => category = ExpenseCategory.Savings);
        Assert.DoesNotThrow(() => category = ExpenseCategory.Other);
    }

    [Test]
    public void ExpenseCategory_DefaultValue_IsHousing()
    {
        // Arrange
        ExpenseCategory category = default;

        // Assert
        Assert.That(category, Is.EqualTo(ExpenseCategory.Housing));
    }

    [Test]
    public void ExpenseCategory_CanCompareValues()
    {
        // Arrange
        var housing = ExpenseCategory.Housing;
        var food = ExpenseCategory.Food;
        var other = ExpenseCategory.Other;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(housing, Is.Not.EqualTo(food));
            Assert.That(food, Is.Not.EqualTo(other));
            Assert.That(housing, Is.EqualTo(ExpenseCategory.Housing));
        });
    }
}
