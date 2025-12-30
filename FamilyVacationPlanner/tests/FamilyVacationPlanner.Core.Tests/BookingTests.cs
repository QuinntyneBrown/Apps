// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyVacationPlanner.Core.Tests;

public class BookingTests
{
    [Test]
    public void Constructor_CreatesBooking_WithDefaultValues()
    {
        // Arrange & Act
        var booking = new Booking();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(booking.BookingId, Is.EqualTo(Guid.Empty));
            Assert.That(booking.TripId, Is.EqualTo(Guid.Empty));
            Assert.That(booking.Trip, Is.Null);
            Assert.That(booking.Type, Is.EqualTo(string.Empty));
            Assert.That(booking.ConfirmationNumber, Is.Null);
            Assert.That(booking.Cost, Is.Null);
            Assert.That(booking.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void BookingId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var booking = new Booking();
        var expectedId = Guid.NewGuid();

        // Act
        booking.BookingId = expectedId;

        // Assert
        Assert.That(booking.BookingId, Is.EqualTo(expectedId));
    }

    [Test]
    public void Type_CanBeSet_AndRetrieved()
    {
        // Arrange
        var booking = new Booking();
        var expectedType = "Hotel";

        // Act
        booking.Type = expectedType;

        // Assert
        Assert.That(booking.Type, Is.EqualTo(expectedType));
    }

    [Test]
    public void ConfirmationNumber_CanBeSet_AndRetrieved()
    {
        // Arrange
        var booking = new Booking();
        var expectedConfirmation = "ABC123XYZ";

        // Act
        booking.ConfirmationNumber = expectedConfirmation;

        // Assert
        Assert.That(booking.ConfirmationNumber, Is.EqualTo(expectedConfirmation));
    }

    [Test]
    public void Cost_CanBeSet_AndRetrieved()
    {
        // Arrange
        var booking = new Booking();
        var expectedCost = 1250.75m;

        // Act
        booking.Cost = expectedCost;

        // Assert
        Assert.That(booking.Cost, Is.EqualTo(expectedCost));
    }

    [Test]
    public void Cost_CanBeNull()
    {
        // Arrange
        var booking = new Booking();

        // Act
        booking.Cost = null;

        // Assert
        Assert.That(booking.Cost, Is.Null);
    }

    [Test]
    public void Booking_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var bookingId = Guid.NewGuid();
        var tripId = Guid.NewGuid();
        var type = "Flight";
        var confirmationNumber = "FL789456";
        var cost = 850.00m;
        var createdAt = DateTime.UtcNow;

        // Act
        var booking = new Booking
        {
            BookingId = bookingId,
            TripId = tripId,
            Type = type,
            ConfirmationNumber = confirmationNumber,
            Cost = cost,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(booking.BookingId, Is.EqualTo(bookingId));
            Assert.That(booking.TripId, Is.EqualTo(tripId));
            Assert.That(booking.Type, Is.EqualTo(type));
            Assert.That(booking.ConfirmationNumber, Is.EqualTo(confirmationNumber));
            Assert.That(booking.Cost, Is.EqualTo(cost));
            Assert.That(booking.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Booking_ForDifferentTypes_CanBeCreated()
    {
        // Arrange & Act
        var hotelBooking = new Booking { Type = "Hotel" };
        var flightBooking = new Booking { Type = "Flight" };
        var carRentalBooking = new Booking { Type = "Car Rental" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(hotelBooking.Type, Is.EqualTo("Hotel"));
            Assert.That(flightBooking.Type, Is.EqualTo("Flight"));
            Assert.That(carRentalBooking.Type, Is.EqualTo("Car Rental"));
        });
    }
}
