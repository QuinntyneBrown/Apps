// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Api.Features.Events;
using ConferenceEventManager.Core;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ConferenceEventManager.Api.Tests.Features.Events;

[TestFixture]
public class CreateEventTests
{
    private IConferenceEventManagerContext _context = null!;
    private IValidator<CreateEvent.Command> _validator = null!;
    private CreateEvent.Handler _handler = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<Infrastructure.Data.ConferenceEventManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new Infrastructure.Data.ConferenceEventManagerContext(options);
        _validator = new CreateEvent.Validator();
        _handler = new CreateEvent.Handler(_context, _validator);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesEvent()
    {
        // Arrange
        var command = new CreateEvent.Command
        {
            UserId = Guid.NewGuid(),
            Name = "Test Conference",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow.AddDays(30),
            EndDate = DateTime.UtcNow.AddDays(32),
            Location = "Seattle, WA",
            IsVirtual = false,
            IsRegistered = true,
            DidAttend = false
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);
        result.EventType.Should().Be(command.EventType);
        result.UserId.Should().Be(command.UserId);

        var eventInDb = await _context.Events.FirstOrDefaultAsync(e => e.EventId == result.EventId);
        eventInDb.Should().NotBeNull();
        eventInDb!.Name.Should().Be(command.Name);
    }

    [Test]
    public void Validator_EmptyName_ShouldFail()
    {
        // Arrange
        var command = new CreateEvent.Command
        {
            UserId = Guid.NewGuid(),
            Name = string.Empty,
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow.AddDays(30),
            EndDate = DateTime.UtcNow.AddDays(32)
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Name");
    }

    [Test]
    public void Validator_EndDateBeforeStartDate_ShouldFail()
    {
        // Arrange
        var command = new CreateEvent.Command
        {
            UserId = Guid.NewGuid(),
            Name = "Test Event",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow.AddDays(32),
            EndDate = DateTime.UtcNow.AddDays(30)
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "EndDate");
    }
}
