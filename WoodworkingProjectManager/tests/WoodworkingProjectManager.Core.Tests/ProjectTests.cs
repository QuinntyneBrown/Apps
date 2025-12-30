namespace WoodworkingProjectManager.Core.Tests;

public class ProjectTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesProject()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Dining Table";
        var description = "Solid oak dining table with extension leaves";
        var status = ProjectStatus.Planned;
        var woodType = WoodType.Oak;
        var startDate = new DateTime(2024, 1, 15);
        var estimatedCost = 500.00m;
        var notes = "Need to order hardware";

        // Act
        var project = new Project
        {
            ProjectId = projectId,
            UserId = userId,
            Name = name,
            Description = description,
            Status = status,
            WoodType = woodType,
            StartDate = startDate,
            EstimatedCost = estimatedCost,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.ProjectId, Is.EqualTo(projectId));
            Assert.That(project.UserId, Is.EqualTo(userId));
            Assert.That(project.Name, Is.EqualTo(name));
            Assert.That(project.Description, Is.EqualTo(description));
            Assert.That(project.Status, Is.EqualTo(status));
            Assert.That(project.WoodType, Is.EqualTo(woodType));
            Assert.That(project.StartDate, Is.EqualTo(startDate));
            Assert.That(project.EstimatedCost, Is.EqualTo(estimatedCost));
            Assert.That(project.Notes, Is.EqualTo(notes));
            Assert.That(project.CompletionDate, Is.Null);
            Assert.That(project.ActualCost, Is.Null);
            Assert.That(project.Materials, Is.Not.Null);
        });
    }

    [Test]
    public void Name_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var project = new Project();

        // Assert
        Assert.That(project.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Description_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var project = new Project();

        // Assert
        Assert.That(project.Description, Is.EqualTo(string.Empty));
    }

    [Test]
    public void MarkAsCompleted_UpdatesStatusAndSetsCompletionDate()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Bookshelf",
            Status = ProjectStatus.InProgress
        };

        // Act
        project.MarkAsCompleted();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.Status, Is.EqualTo(ProjectStatus.Completed));
            Assert.That(project.CompletionDate, Is.Not.Null);
            Assert.That(project.CompletionDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void StartDate_NullValue_IsAllowed()
    {
        // Arrange & Act
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Future Project",
            StartDate = null
        };

        // Assert
        Assert.That(project.StartDate, Is.Null);
    }

    [Test]
    public void CompletionDate_NullValue_IsAllowed()
    {
        // Arrange & Act
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Ongoing Project",
            CompletionDate = null
        };

        // Assert
        Assert.That(project.CompletionDate, Is.Null);
    }

    [Test]
    public void EstimatedCost_NullValue_IsAllowed()
    {
        // Arrange & Act
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            EstimatedCost = null
        };

        // Assert
        Assert.That(project.EstimatedCost, Is.Null);
    }

    [Test]
    public void ActualCost_NullValue_IsAllowed()
    {
        // Arrange & Act
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            ActualCost = null
        };

        // Assert
        Assert.That(project.ActualCost, Is.Null);
    }

    [Test]
    public void Notes_NullValue_IsAllowed()
    {
        // Arrange & Act
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            Notes = null
        };

        // Assert
        Assert.That(project.Notes, Is.Null);
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSetToUtcNow()
    {
        // Arrange & Act
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project"
        };

        // Assert
        Assert.That(project.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void ProjectStatus_AllValues_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var planned = new Project { Status = ProjectStatus.Planned };
            Assert.That(planned.Status, Is.EqualTo(ProjectStatus.Planned));

            var inProgress = new Project { Status = ProjectStatus.InProgress };
            Assert.That(inProgress.Status, Is.EqualTo(ProjectStatus.InProgress));

            var onHold = new Project { Status = ProjectStatus.OnHold };
            Assert.That(onHold.Status, Is.EqualTo(ProjectStatus.OnHold));

            var completed = new Project { Status = ProjectStatus.Completed };
            Assert.That(completed.Status, Is.EqualTo(ProjectStatus.Completed));

            var abandoned = new Project { Status = ProjectStatus.Abandoned };
            Assert.That(abandoned.Status, Is.EqualTo(ProjectStatus.Abandoned));
        });
    }

    [Test]
    public void WoodType_AllValues_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var oak = new Project { WoodType = WoodType.Oak };
            Assert.That(oak.WoodType, Is.EqualTo(WoodType.Oak));

            var maple = new Project { WoodType = WoodType.Maple };
            Assert.That(maple.WoodType, Is.EqualTo(WoodType.Maple));

            var cherry = new Project { WoodType = WoodType.Cherry };
            Assert.That(cherry.WoodType, Is.EqualTo(WoodType.Cherry));

            var walnut = new Project { WoodType = WoodType.Walnut };
            Assert.That(walnut.WoodType, Is.EqualTo(WoodType.Walnut));

            var pine = new Project { WoodType = WoodType.Pine };
            Assert.That(pine.WoodType, Is.EqualTo(WoodType.Pine));

            var cedar = new Project { WoodType = WoodType.Cedar };
            Assert.That(cedar.WoodType, Is.EqualTo(WoodType.Cedar));

            var mahogany = new Project { WoodType = WoodType.Mahogany };
            Assert.That(mahogany.WoodType, Is.EqualTo(WoodType.Mahogany));

            var birch = new Project { WoodType = WoodType.Birch };
            Assert.That(birch.WoodType, Is.EqualTo(WoodType.Birch));

            var ash = new Project { WoodType = WoodType.Ash };
            Assert.That(ash.WoodType, Is.EqualTo(WoodType.Ash));

            var teak = new Project { WoodType = WoodType.Teak };
            Assert.That(teak.WoodType, Is.EqualTo(WoodType.Teak));

            var other = new Project { WoodType = WoodType.Other };
            Assert.That(other.WoodType, Is.EqualTo(WoodType.Other));
        });
    }

    [Test]
    public void Materials_Collection_CanBePopulated()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Coffee Table"
        };

        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            ProjectId = project.ProjectId,
            Name = "Oak Lumber",
            Quantity = 20,
            Unit = "board feet"
        };

        // Act
        project.Materials.Add(material);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.Materials, Has.Count.EqualTo(1));
            Assert.That(project.Materials.First().Name, Is.EqualTo("Oak Lumber"));
        });
    }

    [Test]
    public void Project_WithCosts_CanTrackBudget()
    {
        // Arrange & Act
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Custom Cabinet",
            EstimatedCost = 300.00m,
            ActualCost = 350.00m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.EstimatedCost, Is.EqualTo(300.00m));
            Assert.That(project.ActualCost, Is.EqualTo(350.00m));
            Assert.That(project.ActualCost, Is.GreaterThan(project.EstimatedCost));
        });
    }

    [Test]
    public void Project_WithDates_CanTrackTimeline()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1);
        var completionDate = new DateTime(2024, 3, 15);

        // Act
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Garden Bench",
            Status = ProjectStatus.Completed,
            StartDate = startDate,
            CompletionDate = completionDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.StartDate, Is.EqualTo(startDate));
            Assert.That(project.CompletionDate, Is.EqualTo(completionDate));
            Assert.That(project.CompletionDate, Is.GreaterThan(project.StartDate));
        });
    }

    [Test]
    public void MarkAsCompleted_PlannedProject_ChangesStatus()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Tool Box",
            Status = ProjectStatus.Planned
        };

        // Act
        project.MarkAsCompleted();

        // Assert
        Assert.That(project.Status, Is.EqualTo(ProjectStatus.Completed));
    }
}
