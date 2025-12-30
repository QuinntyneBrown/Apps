// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core.Tests;

public class NeighborTests
{
    [Test]
    public void Constructor_CreatesNeighbor_WithDefaultValues()
    {
        // Arrange & Act
        var neighbor = new Neighbor();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(neighbor.NeighborId, Is.EqualTo(Guid.Empty));
            Assert.That(neighbor.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(neighbor.Name, Is.EqualTo(string.Empty));
            Assert.That(neighbor.Address, Is.Null);
            Assert.That(neighbor.ContactInfo, Is.Null);
            Assert.That(neighbor.Bio, Is.Null);
            Assert.That(neighbor.Interests, Is.Null);
            Assert.That(neighbor.IsVerified, Is.False);
            Assert.That(neighbor.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(neighbor.UpdatedAt, Is.Null);
            Assert.That(neighbor.Recommendations, Is.Not.Null);
            Assert.That(neighbor.Recommendations, Is.Empty);
            Assert.That(neighbor.SentMessages, Is.Not.Null);
            Assert.That(neighbor.SentMessages, Is.Empty);
        });
    }

    [Test]
    public void NeighborId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var neighbor = new Neighbor();
        var expectedId = Guid.NewGuid();

        // Act
        neighbor.NeighborId = expectedId;

        // Assert
        Assert.That(neighbor.NeighborId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var neighbor = new Neighbor();
        var expectedUserId = Guid.NewGuid();

        // Act
        neighbor.UserId = expectedUserId;

        // Assert
        Assert.That(neighbor.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void Name_CanBeSet_AndRetrieved()
    {
        // Arrange
        var neighbor = new Neighbor();
        var expectedName = "Jane Smith";

        // Act
        neighbor.Name = expectedName;

        // Assert
        Assert.That(neighbor.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void Address_CanBeSet_AndRetrieved()
    {
        // Arrange
        var neighbor = new Neighbor();
        var expectedAddress = "123 Main St";

        // Act
        neighbor.Address = expectedAddress;

        // Assert
        Assert.That(neighbor.Address, Is.EqualTo(expectedAddress));
    }

    [Test]
    public void ContactInfo_CanBeSet_AndRetrieved()
    {
        // Arrange
        var neighbor = new Neighbor();
        var expectedContact = "jane@example.com";

        // Act
        neighbor.ContactInfo = expectedContact;

        // Assert
        Assert.That(neighbor.ContactInfo, Is.EqualTo(expectedContact));
    }

    [Test]
    public void Bio_CanBeSet_AndRetrieved()
    {
        // Arrange
        var neighbor = new Neighbor();
        var expectedBio = "Friendly neighbor who loves gardening";

        // Act
        neighbor.Bio = expectedBio;

        // Assert
        Assert.That(neighbor.Bio, Is.EqualTo(expectedBio));
    }

    [Test]
    public void Interests_CanBeSet_AndRetrieved()
    {
        // Arrange
        var neighbor = new Neighbor();
        var expectedInterests = "Gardening, Cooking, Reading";

        // Act
        neighbor.Interests = expectedInterests;

        // Assert
        Assert.That(neighbor.Interests, Is.EqualTo(expectedInterests));
    }

    [Test]
    public void IsVerified_DefaultsToFalse()
    {
        // Arrange & Act
        var neighbor = new Neighbor();

        // Assert
        Assert.That(neighbor.IsVerified, Is.False);
    }

    [Test]
    public void Verify_SetsIsVerifiedToTrue()
    {
        // Arrange
        var neighbor = new Neighbor
        {
            NeighborId = Guid.NewGuid(),
            Name = "Test Neighbor"
        };

        // Act
        neighbor.Verify();

        // Assert
        Assert.That(neighbor.IsVerified, Is.True);
    }

    [Test]
    public void Verify_SetsUpdatedAtToCurrentTime()
    {
        // Arrange
        var neighbor = new Neighbor
        {
            NeighborId = Guid.NewGuid(),
            Name = "Test Neighbor"
        };
        var beforeVerify = DateTime.UtcNow;

        // Act
        neighbor.Verify();
        var afterVerify = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(neighbor.UpdatedAt, Is.Not.Null);
            Assert.That(neighbor.UpdatedAt!.Value, Is.GreaterThanOrEqualTo(beforeVerify));
            Assert.That(neighbor.UpdatedAt!.Value, Is.LessThanOrEqualTo(afterVerify));
        });
    }

    [Test]
    public void Recommendations_CanBePopulated()
    {
        // Arrange
        var neighbor = new Neighbor();
        var rec1 = new Recommendation { RecommendationId = Guid.NewGuid() };
        var rec2 = new Recommendation { RecommendationId = Guid.NewGuid() };

        // Act
        neighbor.Recommendations.Add(rec1);
        neighbor.Recommendations.Add(rec2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(neighbor.Recommendations.Count, Is.EqualTo(2));
            Assert.That(neighbor.Recommendations, Does.Contain(rec1));
            Assert.That(neighbor.Recommendations, Does.Contain(rec2));
        });
    }

    [Test]
    public void SentMessages_CanBePopulated()
    {
        // Arrange
        var neighbor = new Neighbor();
        var msg1 = new Message { MessageId = Guid.NewGuid() };
        var msg2 = new Message { MessageId = Guid.NewGuid() };

        // Act
        neighbor.SentMessages.Add(msg1);
        neighbor.SentMessages.Add(msg2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(neighbor.SentMessages.Count, Is.EqualTo(2));
            Assert.That(neighbor.SentMessages, Does.Contain(msg1));
            Assert.That(neighbor.SentMessages, Does.Contain(msg2));
        });
    }
}
