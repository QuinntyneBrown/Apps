// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core.Tests;

public class InteractionTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInteraction()
    {
        // Arrange & Act
        var interaction = new Interaction();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(interaction.InteractionId, Is.EqualTo(Guid.Empty));
            Assert.That(interaction.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(interaction.ContactId, Is.EqualTo(Guid.Empty));
            Assert.That(interaction.InteractionType, Is.EqualTo(string.Empty));
            Assert.That(interaction.InteractionDate, Is.EqualTo(default(DateTime)));
            Assert.That(interaction.Subject, Is.Null);
            Assert.That(interaction.Notes, Is.Null);
            Assert.That(interaction.Outcome, Is.Null);
            Assert.That(interaction.DurationMinutes, Is.Null);
            Assert.That(interaction.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(interaction.UpdatedAt, Is.Null);
            Assert.That(interaction.Contact, Is.Null);
        });
    }

    [Test]
    public void Constructor_WithValidParameters_CreatesInteraction()
    {
        // Arrange
        var interactionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var contactId = Guid.NewGuid();
        var interactionType = "Email";
        var interactionDate = new DateTime(2024, 6, 15, 14, 30, 0);
        var subject = "Project Discussion";
        var notes = "Discussed project timeline";

        // Act
        var interaction = new Interaction
        {
            InteractionId = interactionId,
            UserId = userId,
            ContactId = contactId,
            InteractionType = interactionType,
            InteractionDate = interactionDate,
            Subject = subject,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(interaction.InteractionId, Is.EqualTo(interactionId));
            Assert.That(interaction.UserId, Is.EqualTo(userId));
            Assert.That(interaction.ContactId, Is.EqualTo(contactId));
            Assert.That(interaction.InteractionType, Is.EqualTo(interactionType));
            Assert.That(interaction.InteractionDate, Is.EqualTo(interactionDate));
            Assert.That(interaction.Subject, Is.EqualTo(subject));
            Assert.That(interaction.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void Constructor_EmailInteraction_SetsCorrectType()
    {
        // Arrange & Act
        var interaction = new Interaction
        {
            InteractionType = "Email",
            Subject = "Follow-up email",
            InteractionDate = DateTime.UtcNow
        };

        // Assert
        Assert.That(interaction.InteractionType, Is.EqualTo("Email"));
    }

    [Test]
    public void Constructor_MeetingInteraction_SetsCorrectTypeAndDuration()
    {
        // Arrange & Act
        var interaction = new Interaction
        {
            InteractionType = "Meeting",
            Subject = "Quarterly Review",
            DurationMinutes = 60,
            InteractionDate = DateTime.UtcNow
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(interaction.InteractionType, Is.EqualTo("Meeting"));
            Assert.That(interaction.DurationMinutes, Is.EqualTo(60));
        });
    }

    [Test]
    public void Constructor_CallInteraction_SetsCorrectTypeAndDuration()
    {
        // Arrange & Act
        var interaction = new Interaction
        {
            InteractionType = "Call",
            Subject = "Quick check-in",
            DurationMinutes = 15,
            InteractionDate = DateTime.UtcNow
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(interaction.InteractionType, Is.EqualTo("Call"));
            Assert.That(interaction.DurationMinutes, Is.EqualTo(15));
        });
    }

    [Test]
    public void Constructor_MessageInteraction_SetsCorrectType()
    {
        // Arrange & Act
        var interaction = new Interaction
        {
            InteractionType = "Message",
            Subject = "LinkedIn message",
            InteractionDate = DateTime.UtcNow
        };

        // Assert
        Assert.That(interaction.InteractionType, Is.EqualTo("Message"));
    }

    [Test]
    public void UpdateOutcome_ValidOutcome_UpdatesOutcomeAndTimestamp()
    {
        // Arrange
        var interaction = new Interaction();
        var outcome = "Meeting scheduled for next week";
        var beforeUpdate = DateTime.UtcNow;

        // Act
        interaction.UpdateOutcome(outcome);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(interaction.Outcome, Is.EqualTo(outcome));
            Assert.That(interaction.UpdatedAt, Is.Not.Null);
            Assert.That(interaction.UpdatedAt.Value, Is.GreaterThanOrEqualTo(beforeUpdate));
            Assert.That(interaction.UpdatedAt.Value, Is.LessThanOrEqualTo(DateTime.UtcNow));
        });
    }

    [Test]
    public void UpdateOutcome_EmptyOutcome_SetsEmptyOutcome()
    {
        // Arrange
        var interaction = new Interaction { Outcome = "Previous outcome" };

        // Act
        interaction.UpdateOutcome(string.Empty);

        // Assert
        Assert.That(interaction.Outcome, Is.EqualTo(string.Empty));
    }

    [Test]
    public void UpdateOutcome_MultipleTimes_UpdatesEachTime()
    {
        // Arrange
        var interaction = new Interaction();

        // Act & Assert
        interaction.UpdateOutcome("First outcome");
        Assert.That(interaction.Outcome, Is.EqualTo("First outcome"));
        var firstUpdate = interaction.UpdatedAt;

        Thread.Sleep(10); // Ensure time difference

        interaction.UpdateOutcome("Second outcome");
        Assert.That(interaction.Outcome, Is.EqualTo("Second outcome"));
        Assert.That(interaction.UpdatedAt, Is.GreaterThan(firstUpdate));
    }

    [Test]
    public void Interaction_WithAllOptionalProperties_SetsAllProperties()
    {
        // Arrange & Act
        var interaction = new Interaction
        {
            InteractionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ContactId = Guid.NewGuid(),
            InteractionType = "Meeting",
            InteractionDate = new DateTime(2024, 6, 15, 10, 0, 0),
            Subject = "Project Kickoff",
            Notes = "Discussed project scope and timeline",
            Outcome = "Agreed on deliverables",
            DurationMinutes = 90
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(interaction.Subject, Is.EqualTo("Project Kickoff"));
            Assert.That(interaction.Notes, Is.EqualTo("Discussed project scope and timeline"));
            Assert.That(interaction.Outcome, Is.EqualTo("Agreed on deliverables"));
            Assert.That(interaction.DurationMinutes, Is.EqualTo(90));
        });
    }

    [Test]
    public void Interaction_LongDuration_StoresCorrectly()
    {
        // Arrange & Act
        var interaction = new Interaction
        {
            InteractionType = "Meeting",
            DurationMinutes = 480 // 8 hours
        };

        // Assert
        Assert.That(interaction.DurationMinutes, Is.EqualTo(480));
    }

    [Test]
    public void Interaction_ShortDuration_StoresCorrectly()
    {
        // Arrange & Act
        var interaction = new Interaction
        {
            InteractionType = "Call",
            DurationMinutes = 5
        };

        // Assert
        Assert.That(interaction.DurationMinutes, Is.EqualTo(5));
    }

    [Test]
    public void Interaction_NoDuration_RemainsNull()
    {
        // Arrange & Act
        var interaction = new Interaction
        {
            InteractionType = "Email"
        };

        // Assert
        Assert.That(interaction.DurationMinutes, Is.Null);
    }
}
