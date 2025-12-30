namespace DailyJournalingApp.Core.Tests;

public class MoodTests
{
    [Test]
    public void Mood_VeryHappy_HasCorrectValue()
    {
        // Arrange & Act
        var mood = Mood.VeryHappy;

        // Assert
        Assert.That((int)mood, Is.EqualTo(0));
    }

    [Test]
    public void Mood_Happy_HasCorrectValue()
    {
        // Arrange & Act
        var mood = Mood.Happy;

        // Assert
        Assert.That((int)mood, Is.EqualTo(1));
    }

    [Test]
    public void Mood_Neutral_HasCorrectValue()
    {
        // Arrange & Act
        var mood = Mood.Neutral;

        // Assert
        Assert.That((int)mood, Is.EqualTo(2));
    }

    [Test]
    public void Mood_Sad_HasCorrectValue()
    {
        // Arrange & Act
        var mood = Mood.Sad;

        // Assert
        Assert.That((int)mood, Is.EqualTo(3));
    }

    [Test]
    public void Mood_VerySad_HasCorrectValue()
    {
        // Arrange & Act
        var mood = Mood.VerySad;

        // Assert
        Assert.That((int)mood, Is.EqualTo(4));
    }

    [Test]
    public void Mood_Anxious_HasCorrectValue()
    {
        // Arrange & Act
        var mood = Mood.Anxious;

        // Assert
        Assert.That((int)mood, Is.EqualTo(5));
    }

    [Test]
    public void Mood_Calm_HasCorrectValue()
    {
        // Arrange & Act
        var mood = Mood.Calm;

        // Assert
        Assert.That((int)mood, Is.EqualTo(6));
    }

    [Test]
    public void Mood_Energetic_HasCorrectValue()
    {
        // Arrange & Act
        var mood = Mood.Energetic;

        // Assert
        Assert.That((int)mood, Is.EqualTo(7));
    }

    [Test]
    public void Mood_Tired_HasCorrectValue()
    {
        // Arrange & Act
        var mood = Mood.Tired;

        // Assert
        Assert.That((int)mood, Is.EqualTo(8));
    }

    [Test]
    public void Mood_CanBeAssignedToProperty()
    {
        // Arrange
        var entry = new JournalEntry();

        // Act
        entry.Mood = Mood.Happy;

        // Assert
        Assert.That(entry.Mood, Is.EqualTo(Mood.Happy));
    }

    [Test]
    public void Mood_AllValuesAreUnique()
    {
        // Arrange
        var values = Enum.GetValues<Mood>();

        // Act
        var uniqueValues = values.Distinct().ToList();

        // Assert
        Assert.That(uniqueValues.Count, Is.EqualTo(values.Length));
    }
}
