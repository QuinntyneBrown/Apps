// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SportsTeamFollowingTracker.Core.Tests;

public class SportTests
{
    [Test]
    public void Sport_Football_HasValue0()
    {
        // Arrange & Act
        var sport = Sport.Football;

        // Assert
        Assert.That((int)sport, Is.EqualTo(0));
    }

    [Test]
    public void Sport_Basketball_HasValue1()
    {
        // Arrange & Act
        var sport = Sport.Basketball;

        // Assert
        Assert.That((int)sport, Is.EqualTo(1));
    }

    [Test]
    public void Sport_Baseball_HasValue2()
    {
        // Arrange & Act
        var sport = Sport.Baseball;

        // Assert
        Assert.That((int)sport, Is.EqualTo(2));
    }

    [Test]
    public void Sport_Hockey_HasValue3()
    {
        // Arrange & Act
        var sport = Sport.Hockey;

        // Assert
        Assert.That((int)sport, Is.EqualTo(3));
    }

    [Test]
    public void Sport_Soccer_HasValue4()
    {
        // Arrange & Act
        var sport = Sport.Soccer;

        // Assert
        Assert.That((int)sport, Is.EqualTo(4));
    }

    [Test]
    public void Sport_Tennis_HasValue5()
    {
        // Arrange & Act
        var sport = Sport.Tennis;

        // Assert
        Assert.That((int)sport, Is.EqualTo(5));
    }

    [Test]
    public void Sport_Golf_HasValue6()
    {
        // Arrange & Act
        var sport = Sport.Golf;

        // Assert
        Assert.That((int)sport, Is.EqualTo(6));
    }

    [Test]
    public void Sport_Rugby_HasValue7()
    {
        // Arrange & Act
        var sport = Sport.Rugby;

        // Assert
        Assert.That((int)sport, Is.EqualTo(7));
    }

    [Test]
    public void Sport_Cricket_HasValue8()
    {
        // Arrange & Act
        var sport = Sport.Cricket;

        // Assert
        Assert.That((int)sport, Is.EqualTo(8));
    }

    [Test]
    public void Sport_Other_HasValue9()
    {
        // Arrange & Act
        var sport = Sport.Other;

        // Assert
        Assert.That((int)sport, Is.EqualTo(9));
    }

    [Test]
    public void Sport_AllValues_CanBeEnumerated()
    {
        // Arrange & Act
        var values = Enum.GetValues<Sport>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(values, Has.Length.EqualTo(10));
            Assert.That(values, Contains.Item(Sport.Football));
            Assert.That(values, Contains.Item(Sport.Basketball));
            Assert.That(values, Contains.Item(Sport.Baseball));
            Assert.That(values, Contains.Item(Sport.Hockey));
            Assert.That(values, Contains.Item(Sport.Soccer));
            Assert.That(values, Contains.Item(Sport.Tennis));
            Assert.That(values, Contains.Item(Sport.Golf));
            Assert.That(values, Contains.Item(Sport.Rugby));
            Assert.That(values, Contains.Item(Sport.Cricket));
            Assert.That(values, Contains.Item(Sport.Other));
        });
    }

    [Test]
    public void Sport_CanCompareValues_InDefinedOrder()
    {
        // Arrange & Act & Assert
        Assert.That(Sport.Football < Sport.Basketball, Is.True);
        Assert.That(Sport.Basketball < Sport.Baseball, Is.True);
        Assert.That(Sport.Baseball < Sport.Hockey, Is.True);
        Assert.That(Sport.Hockey < Sport.Soccer, Is.True);
        Assert.That(Sport.Soccer < Sport.Tennis, Is.True);
        Assert.That(Sport.Tennis < Sport.Golf, Is.True);
        Assert.That(Sport.Golf < Sport.Rugby, Is.True);
        Assert.That(Sport.Rugby < Sport.Cricket, Is.True);
        Assert.That(Sport.Cricket < Sport.Other, Is.True);
    }
}
