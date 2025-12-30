namespace NeighborhoodSocialNetwork.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void NeighborDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var neighbor = new Neighbor
        {
            NeighborId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "John Doe",
            Address = "123 Main St",
            ContactInfo = "john@example.com",
            Bio = "Friendly neighbor",
            Interests = "Gardening, Cooking",
            IsVerified = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = neighbor.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.NeighborId, Is.EqualTo(neighbor.NeighborId));
            Assert.That(dto.UserId, Is.EqualTo(neighbor.UserId));
            Assert.That(dto.Name, Is.EqualTo(neighbor.Name));
            Assert.That(dto.Address, Is.EqualTo(neighbor.Address));
            Assert.That(dto.ContactInfo, Is.EqualTo(neighbor.ContactInfo));
            Assert.That(dto.Bio, Is.EqualTo(neighbor.Bio));
            Assert.That(dto.Interests, Is.EqualTo(neighbor.Interests));
            Assert.That(dto.IsVerified, Is.EqualTo(neighbor.IsVerified));
            Assert.That(dto.CreatedAt, Is.EqualTo(neighbor.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(neighbor.UpdatedAt));
        });
    }

    [Test]
    public void MessageDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var message = new Message
        {
            MessageId = Guid.NewGuid(),
            SenderNeighborId = Guid.NewGuid(),
            RecipientNeighborId = Guid.NewGuid(),
            Subject = "Hello",
            Content = "Test message content",
            IsRead = false,
            ReadAt = null,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = message.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.MessageId, Is.EqualTo(message.MessageId));
            Assert.That(dto.SenderNeighborId, Is.EqualTo(message.SenderNeighborId));
            Assert.That(dto.RecipientNeighborId, Is.EqualTo(message.RecipientNeighborId));
            Assert.That(dto.Subject, Is.EqualTo(message.Subject));
            Assert.That(dto.Content, Is.EqualTo(message.Content));
            Assert.That(dto.IsRead, Is.EqualTo(message.IsRead));
            Assert.That(dto.ReadAt, Is.EqualTo(message.ReadAt));
            Assert.That(dto.CreatedAt, Is.EqualTo(message.CreatedAt));
        });
    }

    [Test]
    public void RecommendationDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var recommendation = new Recommendation
        {
            RecommendationId = Guid.NewGuid(),
            NeighborId = Guid.NewGuid(),
            Title = "Best Pizza Place",
            Description = "Great pizza and friendly service",
            RecommendationType = RecommendationType.Restaurant,
            BusinessName = "Pizza Palace",
            Location = "123 Food St",
            Rating = 5,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = recommendation.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.RecommendationId, Is.EqualTo(recommendation.RecommendationId));
            Assert.That(dto.NeighborId, Is.EqualTo(recommendation.NeighborId));
            Assert.That(dto.Title, Is.EqualTo(recommendation.Title));
            Assert.That(dto.Description, Is.EqualTo(recommendation.Description));
            Assert.That(dto.RecommendationType, Is.EqualTo(recommendation.RecommendationType));
            Assert.That(dto.BusinessName, Is.EqualTo(recommendation.BusinessName));
            Assert.That(dto.Location, Is.EqualTo(recommendation.Location));
            Assert.That(dto.Rating, Is.EqualTo(recommendation.Rating));
            Assert.That(dto.CreatedAt, Is.EqualTo(recommendation.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(recommendation.UpdatedAt));
        });
    }

    [Test]
    public void EventDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var @event = new Event
        {
            EventId = Guid.NewGuid(),
            CreatedByNeighborId = Guid.NewGuid(),
            Title = "Neighborhood BBQ",
            Description = "Annual summer barbecue",
            EventDateTime = DateTime.UtcNow.AddDays(7),
            Location = "Community Park",
            IsPublic = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = @event.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.EventId, Is.EqualTo(@event.EventId));
            Assert.That(dto.CreatedByNeighborId, Is.EqualTo(@event.CreatedByNeighborId));
            Assert.That(dto.Title, Is.EqualTo(@event.Title));
            Assert.That(dto.Description, Is.EqualTo(@event.Description));
            Assert.That(dto.EventDateTime, Is.EqualTo(@event.EventDateTime));
            Assert.That(dto.Location, Is.EqualTo(@event.Location));
            Assert.That(dto.IsPublic, Is.EqualTo(@event.IsPublic));
            Assert.That(dto.CreatedAt, Is.EqualTo(@event.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(@event.UpdatedAt));
        });
    }

    [Test]
    public async Task CreateNeighborCommand_CreatesNeighbor()
    {
        // Arrange
        var neighbors = new List<Neighbor>();
        var mockDbSet = TestHelpers.CreateMockDbSet(neighbors);
        var mockContext = new Mock<INeighborhoodSocialNetworkContext>();
        mockContext.Setup(c => c.Neighbors).Returns(mockDbSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var mockLogger = new Mock<ILogger<CreateNeighborCommandHandler>>();
        var handler = new CreateNeighborCommandHandler(mockContext.Object, mockLogger.Object);

        var command = new CreateNeighborCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Jane Smith",
            Address = "456 Oak Ave",
            ContactInfo = "jane@example.com",
            Bio = "New to the neighborhood",
            Interests = "Reading, Hiking",
            IsVerified = false,
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(neighbors.Count, Is.EqualTo(1));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateMessageCommand_CreatesMessage()
    {
        // Arrange
        var messages = new List<Message>();
        var mockDbSet = TestHelpers.CreateMockDbSet(messages);
        var mockContext = new Mock<INeighborhoodSocialNetworkContext>();
        mockContext.Setup(c => c.Messages).Returns(mockDbSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var mockLogger = new Mock<ILogger<CreateMessageCommandHandler>>();
        var handler = new CreateMessageCommandHandler(mockContext.Object, mockLogger.Object);

        var command = new CreateMessageCommand
        {
            SenderNeighborId = Guid.NewGuid(),
            RecipientNeighborId = Guid.NewGuid(),
            Subject = "Welcome",
            Content = "Welcome to the neighborhood!",
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Subject, Is.EqualTo(command.Subject));
        Assert.That(result.Content, Is.EqualTo(command.Content));
        Assert.That(result.IsRead, Is.False);
        Assert.That(messages.Count, Is.EqualTo(1));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateRecommendationCommand_CreatesRecommendation()
    {
        // Arrange
        var recommendations = new List<Recommendation>();
        var mockDbSet = TestHelpers.CreateMockDbSet(recommendations);
        var mockContext = new Mock<INeighborhoodSocialNetworkContext>();
        mockContext.Setup(c => c.Recommendations).Returns(mockDbSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var mockLogger = new Mock<ILogger<CreateRecommendationCommandHandler>>();
        var handler = new CreateRecommendationCommandHandler(mockContext.Object, mockLogger.Object);

        var command = new CreateRecommendationCommand
        {
            NeighborId = Guid.NewGuid(),
            Title = "Best Coffee Shop",
            Description = "Great coffee and atmosphere",
            RecommendationType = RecommendationType.Shop,
            BusinessName = "Java House",
            Location = "789 Coffee Ln",
            Rating = 4,
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.RecommendationType, Is.EqualTo(command.RecommendationType));
        Assert.That(result.Rating, Is.EqualTo(command.Rating));
        Assert.That(recommendations.Count, Is.EqualTo(1));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateEventCommand_CreatesEvent()
    {
        // Arrange
        var events = new List<Event>();
        var mockDbSet = TestHelpers.CreateMockDbSet(events);
        var mockContext = new Mock<INeighborhoodSocialNetworkContext>();
        mockContext.Setup(c => c.Events).Returns(mockDbSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var mockLogger = new Mock<ILogger<CreateEventCommandHandler>>();
        var handler = new CreateEventCommandHandler(mockContext.Object, mockLogger.Object);

        var command = new CreateEventCommand
        {
            CreatedByNeighborId = Guid.NewGuid(),
            Title = "Block Party",
            Description = "End of summer celebration",
            EventDateTime = DateTime.UtcNow.AddDays(14),
            Location = "Main Street",
            IsPublic = true,
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.EventDateTime, Is.EqualTo(command.EventDateTime));
        Assert.That(result.IsPublic, Is.True);
        Assert.That(events.Count, Is.EqualTo(1));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
