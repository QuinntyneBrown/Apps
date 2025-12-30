// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Core.Tests;

public class MealPlanCreatedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        var mealPlanId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var mealDate = new DateTime(2024, 6, 15);
        var mealType = "Dinner";

        var eventData = new MealPlanCreatedEvent
        {
            MealPlanId = mealPlanId,
            UserId = userId,
            MealDate = mealDate,
            MealType = mealType
        };

        Assert.Multiple(() =>
        {
            Assert.That(eventData.MealPlanId, Is.EqualTo(mealPlanId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.MealDate, Is.EqualTo(mealDate));
            Assert.That(eventData.MealType, Is.EqualTo(mealType));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var mealPlanId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new MealPlanCreatedEvent { MealPlanId = mealPlanId, UserId = userId, MealDate = DateTime.Today, MealType = "Lunch", Timestamp = timestamp };
        var event2 = new MealPlanCreatedEvent { MealPlanId = mealPlanId, UserId = userId, MealDate = DateTime.Today, MealType = "Lunch", Timestamp = timestamp };

        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        var event1 = new MealPlanCreatedEvent { MealPlanId = Guid.NewGuid(), UserId = Guid.NewGuid(), MealDate = DateTime.Today, MealType = "Breakfast" };
        var event2 = new MealPlanCreatedEvent { MealPlanId = Guid.NewGuid(), UserId = Guid.NewGuid(), MealDate = DateTime.Today.AddDays(1), MealType = "Dinner" };

        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
