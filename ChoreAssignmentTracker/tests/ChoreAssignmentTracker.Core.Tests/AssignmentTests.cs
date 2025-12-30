// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Core.Tests;

public class AssignmentTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var assignment = new Assignment();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(assignment.IsCompleted, Is.False);
            Assert.That(assignment.IsVerified, Is.False);
            Assert.That(assignment.PointsEarned, Is.EqualTo(0));
            Assert.That(assignment.CompletedDate, Is.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var assignmentId = Guid.NewGuid();
        var choreId = Guid.NewGuid();
        var familyMemberId = Guid.NewGuid();
        var assignedDate = DateTime.UtcNow;
        var dueDate = DateTime.UtcNow.AddDays(1);

        // Act
        var assignment = new Assignment
        {
            AssignmentId = assignmentId,
            ChoreId = choreId,
            FamilyMemberId = familyMemberId,
            AssignedDate = assignedDate,
            DueDate = dueDate,
            Notes = "Test notes",
            PointsEarned = 10
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(assignment.AssignmentId, Is.EqualTo(assignmentId));
            Assert.That(assignment.ChoreId, Is.EqualTo(choreId));
            Assert.That(assignment.FamilyMemberId, Is.EqualTo(familyMemberId));
            Assert.That(assignment.AssignedDate, Is.EqualTo(assignedDate));
            Assert.That(assignment.DueDate, Is.EqualTo(dueDate));
            Assert.That(assignment.Notes, Is.EqualTo("Test notes"));
            Assert.That(assignment.PointsEarned, Is.EqualTo(10));
        });
    }

    [Test]
    public void Complete_SetsIsCompletedTrue()
    {
        // Arrange
        var assignment = new Assignment
        {
            IsCompleted = false,
            CompletedDate = null
        };

        // Act
        assignment.Complete();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(assignment.IsCompleted, Is.True);
            Assert.That(assignment.CompletedDate, Is.Not.Null);
            Assert.That(assignment.CompletedDate.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Complete_AlreadyCompleted_UpdatesCompletedDate()
    {
        // Arrange
        var originalDate = DateTime.UtcNow.AddDays(-1);
        var assignment = new Assignment
        {
            IsCompleted = true,
            CompletedDate = originalDate
        };

        // Act
        assignment.Complete();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(assignment.IsCompleted, Is.True);
            Assert.That(assignment.CompletedDate, Is.Not.EqualTo(originalDate));
            Assert.That(assignment.CompletedDate.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Verify_SetsIsVerifiedTrueAndPoints()
    {
        // Arrange
        var assignment = new Assignment
        {
            IsVerified = false,
            PointsEarned = 0
        };

        // Act
        assignment.Verify(15);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(assignment.IsVerified, Is.True);
            Assert.That(assignment.PointsEarned, Is.EqualTo(15));
        });
    }

    [Test]
    public void Verify_ZeroPoints_SetsVerifiedWithZeroPoints()
    {
        // Arrange
        var assignment = new Assignment
        {
            IsVerified = false,
            PointsEarned = 0
        };

        // Act
        assignment.Verify(0);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(assignment.IsVerified, Is.True);
            Assert.That(assignment.PointsEarned, Is.EqualTo(0));
        });
    }

    [Test]
    public void Verify_OverwritesPreviousPoints()
    {
        // Arrange
        var assignment = new Assignment
        {
            IsVerified = false,
            PointsEarned = 10
        };

        // Act
        assignment.Verify(20);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(assignment.IsVerified, Is.True);
            Assert.That(assignment.PointsEarned, Is.EqualTo(20));
        });
    }

    [Test]
    public void IsOverdue_NotCompletedAndPastDue_ReturnsTrue()
    {
        // Arrange
        var assignment = new Assignment
        {
            IsCompleted = false,
            DueDate = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var result = assignment.IsOverdue();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsOverdue_NotCompletedAndFutureDue_ReturnsFalse()
    {
        // Arrange
        var assignment = new Assignment
        {
            IsCompleted = false,
            DueDate = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var result = assignment.IsOverdue();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOverdue_CompletedAndPastDue_ReturnsFalse()
    {
        // Arrange
        var assignment = new Assignment
        {
            IsCompleted = true,
            DueDate = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var result = assignment.IsOverdue();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOverdue_NotCompletedAndDueNow_ReturnsFalse()
    {
        // Arrange
        var assignment = new Assignment
        {
            IsCompleted = false,
            DueDate = DateTime.UtcNow.AddSeconds(1)
        };

        // Act
        var result = assignment.IsOverdue();

        // Assert
        Assert.That(result, Is.False);
    }
}
