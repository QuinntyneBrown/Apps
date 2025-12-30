// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PokerGameTracker.Core.Tests;

public class GameTypeTests
{
    [Test]
    public void TexasHoldem_HasCorrectValue()
    {
        // Assert
        Assert.That((int)GameType.TexasHoldem, Is.EqualTo(0));
    }

    [Test]
    public void OmahaHoldem_HasCorrectValue()
    {
        // Assert
        Assert.That((int)GameType.OmahaHoldem, Is.EqualTo(1));
    }

    [Test]
    public void SevenCardStud_HasCorrectValue()
    {
        // Assert
        Assert.That((int)GameType.SevenCardStud, Is.EqualTo(2));
    }

    [Test]
    public void FiveCardDraw_HasCorrectValue()
    {
        // Assert
        Assert.That((int)GameType.FiveCardDraw, Is.EqualTo(3));
    }

    [Test]
    public void Tournament_HasCorrectValue()
    {
        // Assert
        Assert.That((int)GameType.Tournament, Is.EqualTo(4));
    }

    [Test]
    public void CashGame_HasCorrectValue()
    {
        // Assert
        Assert.That((int)GameType.CashGame, Is.EqualTo(5));
    }

    [Test]
    public void Other_HasCorrectValue()
    {
        // Assert
        Assert.That((int)GameType.Other, Is.EqualTo(6));
    }

    [Test]
    public void AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var texasHoldem = GameType.TexasHoldem;
        var omahaHoldem = GameType.OmahaHoldem;
        var sevenCardStud = GameType.SevenCardStud;
        var fiveCardDraw = GameType.FiveCardDraw;
        var tournament = GameType.Tournament;
        var cashGame = GameType.CashGame;
        var other = GameType.Other;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(texasHoldem, Is.EqualTo(GameType.TexasHoldem));
            Assert.That(omahaHoldem, Is.EqualTo(GameType.OmahaHoldem));
            Assert.That(sevenCardStud, Is.EqualTo(GameType.SevenCardStud));
            Assert.That(fiveCardDraw, Is.EqualTo(GameType.FiveCardDraw));
            Assert.That(tournament, Is.EqualTo(GameType.Tournament));
            Assert.That(cashGame, Is.EqualTo(GameType.CashGame));
            Assert.That(other, Is.EqualTo(GameType.Other));
        });
    }
}
