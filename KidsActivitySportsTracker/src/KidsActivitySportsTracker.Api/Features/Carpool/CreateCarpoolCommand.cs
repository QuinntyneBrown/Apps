// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Api.Features.Carpool;

/// <summary>
/// Command to create a new carpool.
/// </summary>
public record CreateCarpoolCommand : IRequest<CarpoolDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? DriverName { get; init; }
    public string? DriverContact { get; init; }
    public DateTime? PickupTime { get; init; }
    public string? PickupLocation { get; init; }
    public DateTime? DropoffTime { get; init; }
    public string? DropoffLocation { get; init; }
    public string? Participants { get; init; }
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for creating a new carpool.
/// </summary>
public class CreateCarpoolCommandHandler : IRequestHandler<CreateCarpoolCommand, CarpoolDto>
{
    private readonly IKidsActivitySportsTrackerContext _context;

    public CreateCarpoolCommandHandler(IKidsActivitySportsTrackerContext context)
    {
        _context = context;
    }

    public async Task<CarpoolDto> Handle(CreateCarpoolCommand request, CancellationToken cancellationToken)
    {
        var carpool = new Core.Carpool
        {
            CarpoolId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            DriverName = request.DriverName,
            DriverContact = request.DriverContact,
            PickupTime = request.PickupTime,
            PickupLocation = request.PickupLocation,
            DropoffTime = request.DropoffTime,
            DropoffLocation = request.DropoffLocation,
            Participants = request.Participants,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Carpools.Add(carpool);
        await _context.SaveChangesAsync(cancellationToken);

        return carpool.ToDto();
    }
}
